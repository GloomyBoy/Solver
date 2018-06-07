using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using EmPuzzleLogic.Analyze;
using EmPuzzleLogic.Entity;
using EmPuzzleLogic.Enums;

namespace EmPuzzleLogic
{
    public class PictureHelper
    {
        private static Dictionary<CellColor, Dictionary<CellType, Image<Bgr, byte>>> _cellTypes =
            new Dictionary<CellColor, Dictionary<CellType, Image<Bgr, byte>>>();
        private static Dictionary<CellColor, Image<Bgr, byte>> _enemyCirclesSmall = new Dictionary<CellColor, Image<Bgr, byte>>();
        private static Dictionary<CellColor, Image<Bgr, byte>> _enemyCirclesMed = new Dictionary<CellColor, Image<Bgr, byte>>();
        private static Dictionary<CellColor, Image<Bgr, byte>> _enemyCirclesLarge = new Dictionary<CellColor, Image<Bgr, byte>>();
        private static Image<Gray, float> _weakPoint;

        static PictureHelper()
        {
            var dir = Environment.CurrentDirectory + "\\Resources\\";
            foreach (var cellType in Enum.GetValues(typeof(CellColor)))
            {
                foreach (var cellSpec in Enum.GetValues(typeof(CellType)))
                {
                    if (File.Exists(dir + $"\\{(CellColor)cellType}_{(CellType)cellSpec}.png"))
                    {
                        if(!_cellTypes.ContainsKey((CellColor)cellType))
                            _cellTypes[(CellColor)cellType] = new Dictionary<CellType, Image<Bgr, byte>>();
                        var bitmap = new Bitmap(Bitmap.FromFile(dir + $"\\{(CellColor)cellType}_{(CellType)cellSpec}.png"));
                        _cellTypes[(CellColor)cellType][(CellType)cellSpec] = new Image<Bgr, byte>(bitmap);
                    }
                }

                if (File.Exists(dir + $"\\{(CellColor) cellType}_small_circle.png"))
                {
                    var bitmap = new Bitmap(Bitmap.FromFile(dir + $"\\{(CellColor)cellType}_small_circle.png"));
                    _enemyCirclesSmall[(CellColor)cellType] = new Image<Bgr, byte>(bitmap);
                }
                if (File.Exists(dir + $"\\{(CellColor)cellType}_large_circle.png"))
                {
                    var bitmap = new Bitmap(Bitmap.FromFile(dir + $"\\{(CellColor)cellType}_large_circle.png"));
                    _enemyCirclesLarge[(CellColor)cellType] = new Image<Bgr, byte>(bitmap);
                }
                if (File.Exists(dir + $"\\{(CellColor)cellType}_med_circle.png"))
                {
                    var bitmap = new Bitmap(Bitmap.FromFile(dir + $"\\{(CellColor)cellType}_med_circle.png"));
                    _enemyCirclesMed[(CellColor)cellType] = new Image<Bgr, byte>(bitmap);
                }
                if (File.Exists(dir + $"\\weak_point.png"))
                {
                    var bitmap = new Bitmap(Bitmap.FromFile(dir + $"\\weak_point.png"));
                    _weakPoint = new Image<Gray, float>(bitmap);
                }
            }
        }

        private static Point _gridStart = new Point(16, 360);

        private static int _cellSize = 60;

        private static int _gridXCount = 7;
        private static int _gridYCount = 5;

        public static void LoadImage(Bitmap image, out Grid grid)
        {
            try
            {
                grid = null;
                if (image == null)
                    return;
                Grid result_grid = new Grid(7, 5);
                System.Drawing.Imaging.PixelFormat format = image.PixelFormat;
                for (int i = 0; i < _gridXCount; i++)
                {
                    for (int j = 0; j < _gridYCount; j++)
                    {
                        Rectangle rect = new Rectangle(_gridStart.X + _cellSize * i, _gridStart.Y + _cellSize * j,
                            _cellSize, _cellSize);
                        var pic = image.Clone(rect, format);
                        var cellType = FindColor(pic);
                        if (cellType.Item1 == CellColor.None)
                            return;
                        result_grid[i, j] = new CellItem(cellType.Item2, cellType.Item1);
                    }
                }

                var imageGray = new Image<Gray, float>(image);
                var points = GetTemplatePosition(imageGray, _weakPoint, 0.5);
                if (points?.Length != 0)
                {
                    result_grid.WeakSlot = (points[0].X - _gridStart.X) / _cellSize;
                    ;
                }

                var result = DetectEnemies(image);
                foreach (var cellColor in result)
                {
                    result_grid._enemies[cellColor.Key] = cellColor.Value;
                }

                grid = result_grid;
            }
            catch (Exception ex)
            {
                Logger.SaveErrorScreen(image);
                grid = null;
            }
        }

        public static void LoadImage(string path, out Grid grid)
        {
            Bitmap image = (Bitmap)Bitmap.FromFile(path);
            LoadImage(image, out grid);
        }

        public static (CellColor, CellType) FindColor(Bitmap bmp)
        {
            var rootImage = new Image<Bgr, byte>(bmp);
            
            foreach (var cellType in _cellTypes)
            {
                foreach (var cellSpec in cellType.Value)
                {
                    if (GetTemplatePosition(rootImage, cellSpec.Value, 0.93, out var diff).Length > 0)
                    {
                        return (cellType.Key, cellSpec.Key);
                    }
                }
            }

            return (CellColor.None, CellType.Regular);
        }

        public static Dictionary<int, CellColor> DetectEnemies(Bitmap grid)
        {
            Dictionary<int, (int, CellColor)> enemies = new Dictionary<int, (int, CellColor)>();

            Brush mask = new SolidBrush(Color.Black);

            bool found = false;
            do
            {
                foreach (var smallCircle in _enemyCirclesSmall)
                {
                    var gridT = new Image<Bgr, byte>(grid);
                    Point[] position = GetTemplatePosition(gridT, smallCircle.Value, 0.65, out var cDiff1);
                    if (position.Length == 0)
                    {
                        found = false;
                        continue;
                    }

                    Graphics g = Graphics.FromImage(grid);
                    g.FillRectangle(mask, new Rectangle(position[0], smallCircle.Value.Size));
                    g.Save();
                    found = true;
                    break;
                }
            } while (found);

            return enemies.ToDictionary(e => e.Key, e => e.Value.Item2);
        }

        public static List<int> GetEnemyPosition(int x, int size)
        {
            switch (size)
            {
                case 1:
                    return (List<int>) (x % 60 > 30 ? Enumerable.Range(x / 60, 3).ToList() : Enumerable.Range(x / 60, 2).Select(i => i).ToList());
            }

            return null;
        }

        public static CellColor GetEnemyColor(Bitmap bmp, int size)
        {
            var dic = _enemyCirclesSmall;
            switch (size)
            {
                case 2: dic = _enemyCirclesMed;
                    break;
                case 3: dic = _enemyCirclesLarge;
                    break;

            }

            double max = 0;
            CellColor color = CellColor.None;
            var testImage = new Image<Bgr, byte>(bmp);
            foreach (var testColor in dic)
            {
                GetTemplatePosition(testImage, testColor.Value, 0.5, out var diff);
                if (max < diff)
                {
                    color = testColor.Key;
                    max = diff;
                }
            }

            return color;
        }

        public static Point[] GetTemplatePosition(Image<Gray, float> source, Image<Gray, float> template, double diff, bool useSobel = false)
        {
            var sourceSobel = useSobel ? source.Sobel(1, 0, 3) : source;
            var templateSobel = useSobel ? template.Sobel(1, 0, 3) : template;

            using (Image<Gray, float> result = sourceSobel.MatchTemplate(templateSobel, TemplateMatchingType.CcoeffNormed))
            {
                double[] minValues, maxValues;
                Point[] minLocations, maxLocations;
                result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);
                if (maxValues.Length > 0 && maxValues[0] > diff)
                {
                    return maxLocations;
                }
            }

            return new Point[0];
        }

        public static Point[] GetTemplatePosition(Image<Bgr, byte> source, Image<Bgr, byte> template, double diff, out double currentDiff)
        {
            currentDiff = 0;
            using (Image<Gray, float> result = source.MatchTemplate(template, TemplateMatchingType.CcoeffNormed))
            {
                double[] minValues, maxValues;
                Point[] minLocations, maxLocations;
                result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);
                if (maxValues.Length > 0 && maxValues[0] > diff)
                {
                    currentDiff = maxValues[0];
                    return maxLocations;
                }
            }

            return new Point[0];
        }
    }
}