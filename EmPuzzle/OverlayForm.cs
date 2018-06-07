using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmPuzzleLogic;
using EmPuzzleLogic.Analyze;
using EmPuzzleLogic.Entity;
using EmPuzzleLogic.Enums;

namespace EmPuzzle
{
    public partial class OverlayForm : Form
    {
        public Color TransparentColor = Color.Magenta;

        public OverlayForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;
            int initialStyle = GetWindowLong(this.Handle, -20);
            SetWindowLong(this.Handle, -20, initialStyle | 0x80000 | 0x20);
            this.TransparencyKey = TransparentColor;

            var baseImage = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(baseImage);
            Brush b = new SolidBrush(Color.Magenta);
            g.FillRectangle(b, 0, 0, baseImage.Width, baseImage.Height);
            g.Save();
            this.BackgroundImage = baseImage;
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("dwmapi.dll")]
        static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, ref int[] pMargins);

        public void Draw(Grid grid, List<SwapResult> swapResult)
        {
            InitBaseImage();
            Graphics graph = Graphics.FromImage(this.BackgroundImage);
            DrawGrid(grid, graph);
            DrawWeakSpot(grid, graph);
            DrawSwaps(swapResult, graph);
            graph.Save();
        }

        private void DrawGrid(Grid grid, Graphics graph)
        {
            
            for (int i = 0; i < grid.Width; i++)
            {
                for (int j = 0; j < grid.Height; j++)
                {
                    var point = new Point(Consts.GRID_LEFT_TOP.X + Consts.CELL_SIZE * i, Consts.GRID_LEFT_TOP.Y + Consts.CELL_SIZE * j);
                    var color = GridDrawer.GetCellColor(grid[i, j].Tag);
                    var pen = new Pen(color, 3);
                    graph.DrawRectangle(pen, point.X, point.Y, Consts.CELL_SIZE - 3, Consts.CELL_SIZE - 3);
                }
            }
        }

        private void DrawWeakSpot(Grid grid, Graphics graph)
        {
            int weakSpotY = 15;
            
            if (grid.WeakSlot >= 0)
            {
                Brush b = new SolidBrush(GridDrawer.GetCellColor(grid._enemies[grid.WeakSlot]));
                var point = new Point(Consts.GRID_LEFT_TOP.X + Consts.CELL_SIZE * grid.WeakSlot + Consts.CELL_SIZE / 2, Consts.GRID_LEFT_TOP.Y - weakSpotY);
                graph.FillPolygon(b, new Point[]
                {
                    new Point(point.X - 15, point.Y),
                    new Point(point.X, point.Y - 15),
                    new Point(point.X + 15, point.Y),
                    new Point(point.X - 15, point.Y)
                });
            }
        }

        private void DrawSwaps(List<SwapResult> results, Graphics graph)
        {
            for (int i = 0; i < results.Count; i++)
            {
                var swap = results[i];
                var pen = new Pen(i == 0 ? Color.Chartreuse : Color.CadetBlue, i == 0 ? 5 : 2);
                Point start = new Point();
                Point end = new Point();
                switch (swap.Direction)
                {
                    case SwapType.Down:
                        start = new Point(
                            Consts.GRID_LEFT_TOP.X + Consts.CELL_SIZE * swap.X + Consts.CELL_SIZE / 2, 
                            Consts.GRID_LEFT_TOP.Y + Consts.CELL_SIZE * swap.Y + Consts.CELL_SIZE / 2 + Consts.CELL_SIZE / 4);
                        end = new Point(
                            Consts.GRID_LEFT_TOP.X + Consts.CELL_SIZE * swap.X + Consts.CELL_SIZE / 2,
                            Consts.GRID_LEFT_TOP.Y + Consts.CELL_SIZE * (swap.Y + 1) + Consts.CELL_SIZE / 2 - Consts.CELL_SIZE / 4);
                        break;
                    case SwapType.Right:
                        start = new Point(
                            Consts.GRID_LEFT_TOP.X + Consts.CELL_SIZE * swap.X + Consts.CELL_SIZE / 2 + Consts.CELL_SIZE / 4,
                            Consts.GRID_LEFT_TOP.Y + Consts.CELL_SIZE * swap.Y + Consts.CELL_SIZE / 2);
                        end = new Point(
                            Consts.GRID_LEFT_TOP.X + Consts.CELL_SIZE * swap.X + Consts.CELL_SIZE / 2 - Consts.CELL_SIZE / 4,
                            Consts.GRID_LEFT_TOP.Y + Consts.CELL_SIZE * (swap.Y + 1) + Consts.CELL_SIZE / 2);
                        break;
                }
                graph.DrawLine(pen, start, end);
            }
        }

        private void InitBaseImage()
        {
            Graphics g = Graphics.FromImage(this.BackgroundImage);
            Brush b = new SolidBrush(Color.Magenta);
            g.FillRectangle(b, 0, 0, this.BackgroundImage.Width, this.BackgroundImage.Height);
            g.Save();
        }
    }
}
