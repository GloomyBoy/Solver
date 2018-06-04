using System.Drawing;
using System.Linq;
using EmPuzzleLogic;
using EmPuzzleLogic.Analyze;
using EmPuzzleLogic.Entity;
using EmPuzzleLogic.Enums;

namespace EmPuzzleLogic
{
    public class GridDrawer
    {
        private static Color GetCellColor(CellColor color)
        {
            switch (color)
            {
                case CellColor.Blue: return Color.Blue;
                case CellColor.Dark: return Color.BlueViolet;
                case CellColor.Green: return Color.Green;
                case CellColor.Light: return Color.Yellow;
                case CellColor.Red: return Color.Red;
            }

            return Color.Black;
        }

        public static Image GetGridImage(Grid grid)
        {
            var result = new Bitmap(420, 300);
            var pic = Graphics.FromImage(result);
            for (int i = 0; i < grid.Width; i++)
            {
                for (int j = 0; j < grid.Height; j++)
                {
                    if (grid[i, j] == null)
                    {
                        continue;

                    }
                    DrawCell(pic, grid[i,j]);
                }
            }
            //GridAnalyzer.GetPossibleSwaps(grid);

            //DrawSwaps(grid, pic);
            return result;
        }

        private static void DrawCell(Graphics graph, CellItem cell)
        {
            var brush = new SolidBrush(GetCellColor(cell.Tag));
            graph.FillRectangle(brush, cell.Position.X * 60, cell.Position.Y * 60, 60, 60);
            graph.DrawRectangle(new Pen(Color.Black, 1), cell.Position.X * 60, cell.Position.Y * 60, 60, 60);
            Pen pen = new Pen(Color.Black, 3);
            switch (cell.Type)
            {
                case CellType.Dragon:
                    PointF[] points = new PointF[4];
                    points[0] = new PointF(30, 10);
                    points[1] = new PointF(50, 30);
                    points[2] = new PointF(30, 50);
                    points[3] = new PointF(10, 30);
                    points[3] = new PointF(30, 10);
                    graph.DrawLines(pen, points);
                    break;
                case CellType.Crystal:
                    graph.DrawEllipse(pen, cell.Position.X * 60 + 15, cell.Position.Y * 60 + 15, 30, 30);
                    break;
            }
            
        }

        /*public void DrawSwaps(Grid grid, Graphics pic)
        {
            var bestSwap = grid.GetBestSwap();
            var swaps = grid.GetSwaps();
            foreach (var swap in swaps)
            {
                var pen = new Pen(swap.Equals(bestSwap.Item1) ? Color.Black : Color.Wheat);
                //var pen = new Pen(Color.Red);
                pen.Width = swap.Equals(bestSwap.Item1) ? 5 : 3;
                Point start = new Point(swap.Col * 60 + 30, swap.Row * 60 + 30);
                Point end = start;
                switch (swap.Direction)
                {
                    case SwapDirection.Bottom:
                        end = new Point(start.X, start.Y + 60);
                        break;
                    case SwapDirection.Left:
                        end = new Point(start.X + 60, start.Y);
                        break;
                }
                pic.DrawLine(pen, start, end);
            }
        }*/
        public static void DrawSwap(Bitmap pbResult, SwapResult swapResult)
        {
            var pen = new Pen(Color.Black, 3);
            Point start = new Point(swapResult.X * 60 + 30, swapResult.Y * 60 + 30);
            Point end = start;
            var g = Graphics.FromImage(pbResult);
            switch (swapResult.Direction)
            {
                case SwapType.Down:
                    end = new Point(start.X, start.Y + 60);
                    break;
                case SwapType.Right:
                    end = new Point(start.X + 60, start.Y);
                    break;
                case SwapType.Point:
                    g.DrawLine(pen, swapResult.X * 60, swapResult.Y * 60, swapResult.X * 60 + 60, swapResult.Y * 60 + 60);
                    g.DrawLine(pen, swapResult.X * 60, swapResult.Y * 60 + 60, swapResult.X * 60 + 60, swapResult.Y * 60);
                    break;
            }
            g.DrawLine(pen, start, end);
        }

        public static Image GetGridEmenies(Grid grid)
        {
            Bitmap bmp = new Bitmap(60 * 7, 60);
            var g = Graphics.FromImage(bmp);
            foreach (var gridEnemy in grid._enemies)
            {
                g.FillRectangle(new SolidBrush(GetCellColor(gridEnemy.Value)), new Rectangle(gridEnemy.Key * 60, 0, 60, 60));
            }

            g.Save(); 
            return (Image) bmp;
        }
    }
}