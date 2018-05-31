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
using EmPuzzleLogic.Enums;

namespace EmPuzzle
{
    public partial class Main : Form
    {
        private Timer _timer;

        public Main()
        {
            InitializeComponent();

            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Enabled = false ;
            _timer.Tick += TimerOnTick;
            //LoadImage();
        }

        private void TimerOnTick(object sender, EventArgs e)
        {
            _timer.Stop();
            LoadImage();
            //CaptureApplication("Bluestacks");
            _timer.Start();
        }

        private void LoadImage()
        {
            var image = CaptureApplication("Bluestacks");
            var grid = PictureHelper.LoadImage(image);
            //pbGameGrid.Image = grid.Image;
            //if (grid.Game.SelectMany(g => g).Any(g => g.Color == CellColor.None)) return;
            //new GridDrawer().DrawSwaps(grid, Graphics.FromImage(grid.Image));
            

            //grid.GetBestSwap();
        }

        public Bitmap CaptureApplication(string procName)
        {
            var proc = Process.GetProcessesByName(procName)[0];
            var rect = new User32.Rect();
            User32.GetWindowRect(proc.MainWindowHandle, ref rect);

            int width = rect.right - rect.left;
            int height = rect.bottom - rect.top;

            var bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            Graphics graphics = Graphics.FromImage(bmp);
            graphics.CopyFromScreen(rect.left, rect.top, 0, 0, new Size(width, height), CopyPixelOperation.SourceCopy);
            pbScreenshot.Image = bmp;
            pbScreenshot.SizeMode = PictureBoxSizeMode.Zoom;
            return bmp;
            //bmp.Save("c:\\tmp\\test.png", ImageFormat.Png);
        }

        private class User32
        {
            [StructLayout(LayoutKind.Sequential)]
            public struct Rect
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            }

            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);
        }

        private void btnSaveSnap_Click(object sender, EventArgs e)
        {
            if (!Directory.GetDirectories(Environment.CurrentDirectory).Contains("Screenshots"))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\Screenshot");
            }
            var image = CaptureApplication("Bluestacks");
            image.Save(Environment.CurrentDirectory + "\\Screenshot\\" + DateTime.Now.ToString("yy-MM-dd_hh-mm-ss") + ".bmp");
        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            if (ofLoadImage.ShowDialog() == DialogResult.OK)
            {
                var bitmap = Bitmap.FromFile(ofLoadImage.FileName);
                var grid = PictureHelper.LoadImage(new Bitmap(bitmap));
                //pbGameGrid.Image = grid.Image;
                //new GridDrawer().DrawSwaps(grid, Graphics.FromImage(grid.Image));
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
    }
}
