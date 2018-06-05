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
                case CellColor.Blue:
                    return Color.LightSkyBlue;
                case CellColor.Dark:
                    return Color.DarkOrchid;// BlueViolet;
                case CellColor.Green:
                    return Color.LightGreen;// Green;
                case CellColor.Light:
                    return Color.Orange;// Yellow;
                case CellColor.Red:
                    return Color.LightCoral;//Red;
            }

            return Color.GhostWhite;
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
                    PointF[] points = new PointF[5];
                    points[0] = new PointF(cell.Position.X * 60 + 30, cell.Position.Y * 60 + 10);
                    points[1] = new PointF(cell.Position.X * 60 + 50, cell.Position.Y * 60 + 30);
                    points[2] = new PointF(cell.Position.X * 60 + 30, cell.Position.Y * 60 + 50);
                    points[3] = new PointF(cell.Position.X * 60 + 10, cell.Position.Y * 60 + 30);
                    points[4] = new PointF(cell.Position.X * 60 + 30, cell.Position.Y * 60 + 10);
                    graph.DrawLines(pen, points);
                    break;
                case CellType.Crystal:
                    graph.DrawEllipse(pen, cell.Position.X * 60 + 15, cell.Position.Y * 60 + 15, 30, 30);
                    break;
            }
            
        }
       
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

        public static Image GetGridEnemies(Grid grid, Image image)
        {
            Bitmap bmp = image as Bitmap;
            var g = Graphics.FromImage(bmp);
            var width = bmp.Width / grid._enemies.Count;
            var height = bmp.Height;
            foreach (var gridEnemy in grid._enemies)
            {
                g.FillRectangle(new SolidBrush(GetCellColor(gridEnemy.Value)), new Rectangle(gridEnemy.Key * width, 0, width, height / 2));
                if (grid.WeakSlot != gridEnemy.Key)
                {
                    g.DrawRectangle(new Pen(Color.Black), new Rectangle(gridEnemy.Key * width, 0, width, height));
                }
                else
                {
                    g.DrawRectangle(new Pen(Color.Red, 3), new Rectangle(gridEnemy.Key * width, 0, width - 3, height - 3));
                }
            }

            g.Save(); 
            return (Image) bmp;
        }

        public static Image GetSwapResultPicture(SwapResult result, Image image)
        {
            Bitmap bmp = image as Bitmap;
            var g = Graphics.FromImage(bmp);
            var width = bmp.Width / result.Result.Count();
            var height = bmp.Height;
            Font f = new Font("Arial", 10, FontStyle.Regular);
            Brush p = new SolidBrush(Color.Black);
            foreach (var resultCell in result.Result)
            {
                var totalCnt = resultCell.Value.Sum(r => r.Value);
                g.DrawString(totalCnt.ToString(), f, p, resultCell.Key * width + 10, 5);
                var items = resultCell.Value.SelectMany(i => Enumerable.Repeat(i.Key, i.Value));
                int currX = 5;
                int currY = height / 2 + 5;
                foreach (var item in items){
                    Rectangle r = new Rectangle(width * resultCell.Key + currX, currY, 5, 5);
                    Brush b = new SolidBrush(GetCellColor(item));
                    g.FillRectangle(b, r);
                    currX += 8;
                    if (currX + 5 >= width)
                    {
                        currX = 5;
                        currY += 8;
                    }
                }
            }
            g.Save();
            return (Image)bmp;
        }
    }
}