using EmPuzzleLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmPuzzleLogic.Analyze;
using EmPuzzleLogic.Entity;
using EmPuzzleLogic.Enums;
using EmPuzzleLogic.Helper;

namespace EmPuzzle
{
    public partial class Main : Form
    {
        private Timer _timer;
        private Point _startLocation;
        private Grid _grid;

        public Main()
        {
            InitializeComponent();

            foreach (var value in Enum.GetValues(typeof(CellColor)))
            {
                cbTitanColor.Items.Add(value);
            }

            _timer = new Timer();
            _timer.Interval = 500;
            _timer.Enabled = false ;
            _timer.Tick += TimerOnTick;
        }

        private void TimerOnTick(object sender, EventArgs e)
        {
            _timer.Stop();
            var image = ProcessHelper.CaptureProcessWindow("Bluestacks");
            pbScreenshot.Image = image;
            pbScreenshot.SizeMode = PictureBoxSizeMode.Zoom;
            LoadImage(image);
            _timer.Start();
        }

        private void LoadImage(Bitmap bitmap)
        {
            if (bitmap == null)
                return;
            PictureHelper.LoadImage(bitmap, out var grid);
            if (grid == null || grid.IsEqual(_grid))
                return;
            
            _grid = grid;
            if (_grid.WeakSlot >= 0 && cbTitanColor.SelectedIndex >= 0)
            {
                foreach (var i in Enumerable.Range(1, 5))
                {
                    _grid._enemies[i] = (CellColor) cbTitanColor.SelectedItem;
                }
            }
            pbGameGrid.Image = GridDrawer.GetGridImage(grid);
            pbEnemies.Image = new Bitmap(pbEnemies.Width, pbEnemies.Height);
            GridDrawer.GetGridEnemies(grid, pbEnemies.Image);
            var results = GridAnalyzer.GetPossibleSwaps(grid);
            results = results.OrderByDescending(r => r, new TitanSwapComparer()).ToList();
            clbSwaps.Items.Clear();
            clbSwaps.Items.AddRange(items: results.ToArray());
            if(results.Count > 0)
                clbSwaps.SetSelected(0, true);
        }

        private void btnSaveSnap_Click(object sender, EventArgs e)
        {
            if (!Directory.GetDirectories(Environment.CurrentDirectory).Contains("Screenshots"))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\Screenshot");
            }
            var image = ProcessHelper.CaptureProcessWindow("Bluestacks");
            if (image == null)
                return;
            image.Save(Environment.CurrentDirectory + "\\Screenshot\\" + DateTime.Now.ToString("yy-MM-dd_hh-mm-ss") + ".bmp");
        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            if (ofLoadImage.ShowDialog() == DialogResult.OK)
            {
                var bitmap = Bitmap.FromFile(ofLoadImage.FileName);
                pbScreenshot.Image = bitmap;
                LoadImage(new Bitmap(bitmap));
            }
        }

        private void cbCapture_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox) sender).Checked)
            {
                _timer.Start();
            }
            else
            {
                _timer.Stop();
            }
        }

        private void pbScreenshot_MouseDown(object sender, MouseEventArgs e)
        {
            _startLocation = e.Location;
        }

        private void pbScreenshot_MouseUp(object sender, MouseEventArgs e)
        {
            var endPoint = e.Location;
            var sP = GetOriginalPoint(_startLocation);
            var eP = GetOriginalPoint(endPoint);
            var rect = new System.Drawing.Rectangle(Math.Min(sP.X, eP.X),
                Math.Min(sP.Y, eP.Y) + 1, Math.Abs(sP.X - eP.X),
                Math.Abs(sP.Y - eP.Y) + 1);
            
            var image = (Bitmap) pbScreenshot.Image;
            var pic = image.Clone(rect, pbScreenshot.Image.PixelFormat);
            pbPreview.Image = pic;

        }

        private Point GetOriginalPoint(Point client)
        {
            Point p = client;//pbScreenshot.PointToClient(client);
            Point unscaled_p = new Point();

            // image and container dimensions
            int w_i = pbScreenshot.Image.Width;
            int h_i = pbScreenshot.Image.Height;
            int w_c = pbScreenshot.Width;
            int h_c = pbScreenshot.Height;
            float imageRatio = w_i / (float)h_i; // image W:H ratio
            float containerRatio = w_c / (float)h_c; // container W:H ratio

            if (imageRatio >= containerRatio)
            {
                // horizontal image
                float scaleFactor = w_c / (float)w_i;
                float scaledHeight = h_i * scaleFactor;
                // calculate gap between top of container and top of image
                float filler = Math.Abs(h_c - scaledHeight) / 2;
                unscaled_p.X = (int)(p.X / scaleFactor);
                unscaled_p.Y = (int)((p.Y - filler) / scaleFactor);
            }
            else
            {
                // vertical image
                float scaleFactor = h_c / (float)h_i;
                float scaledWidth = w_i * scaleFactor;
                float filler = Math.Abs(w_c - scaledWidth) / 2;
                unscaled_p.X = (int)((p.X - filler) / scaleFactor);
                unscaled_p.Y = (int)(p.Y / scaleFactor);
            }

            return unscaled_p;
        }

        private void btnSavePreview_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(Environment.CurrentDirectory + "\\Resources\\"))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\Resources\\");
            }
            var sD = new SaveFileDialog();
            sD.InitialDirectory = Environment.CurrentDirectory + "\\Resources\\";
            if (sD.ShowDialog() == DialogResult.OK)
            {
                pbPreview.Image.Save(sD.FileName);
            }
        }

        private void clbSwaps_SelectedIndexChanged(object sender, EventArgs e)
        {
            var listBox = sender as CheckedListBox;
            var swap = listBox.SelectedItem as SwapResult;
            pbGameGrid.Image = GridDrawer.GetGridImage(_grid);
            GridDrawer.DrawSwap((Bitmap) pbGameGrid.Image, swap);
            pbEnemies.Image = new Bitmap(pbEnemies.Width, pbEnemies.Height);
            GridDrawer.GetGridEnemies(_grid, pbEnemies.Image);
            GridDrawer.GetSwapResultPicture(swap, pbEnemies.Image);
            //var proposed = swap.NextTurnPropositions;
        }
    }
}
