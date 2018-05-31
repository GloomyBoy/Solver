using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
            }
        }

        private static Bitmap ToBlackAndWhite(Bitmap bmp)
        {
            return bmp.Clone(new Rectangle(0, 0, bmp.Width, bmp.Height), PixelFormat.Format1bppIndexed);
        }

        private static Point _gridStart = new Point(16, 360);

        private static int _cellSize = 60;

        private static int _gridXCount = 7;
        private static int _gridYCount = 5;

        public static Grid LoadImage(Bitmap image)
        {
            Grid grid = new Grid(7, 5);
            /*GameGrid grid = new GameGrid()
            {
                Game = new GridCell[_gridXCount][]
            };*/
            System.Drawing.Imaging.PixelFormat format =  image.PixelFormat;
            for (int i = 0; i < _gridXCount; i++)
            {                
                for (int j = 0; j < _gridYCount; j++)
                {
                    Rectangle rect = new Rectangle(_gridStart.X + _cellSize * i, _gridStart.Y + _cellSize * j, _cellSize, _cellSize);
                    var pic = image.Clone(rect, format);
                    var cellType = FindColor(pic);
                    grid[i, j] = new CellItem(cellType.Item2, cellType.Item1);                    
                }
            }
            GridAnalyzer.GetPossibleSwaps(grid);
            return grid;
        }

        public static Grid LoadImage(string path)
        {
            Bitmap image = (Bitmap)Bitmap.FromFile(path);
            return LoadImage(image);
        }

        public static (CellColor, CellType) FindColor(Bitmap bmp)
        {
            var rootImage = new Image<Bgr, byte>(bmp);
            
            foreach (var cellType in _cellTypes)
            {
                foreach (var cellSpec in cellType.Value)
                {
                    if (Detect_object(rootImage, cellSpec.Value, 0.9))
                    {
                        return (cellType.Key, cellSpec.Key);
                    }
                }
            }

            return (CellColor.None, CellType.Regular);
        }

        public static bool Detect_object(Image<Bgr, Byte> Area_Image, Image<Bgr, Byte> image_object, double max_diff)
        {
            bool success = false;
            Point location;
            //Work out padding array size
            Point dftSize = new Point(Area_Image.Width + (image_object.Width * 2), Area_Image.Height + (image_object.Height * 2));
            //Pad the Array with zeros
            using (Image<Bgr, Byte> pad_array = new Image<Bgr, Byte>(dftSize.X, dftSize.Y))
            {
                //copy centre
                pad_array.ROI = new Rectangle(image_object.Width, image_object.Height, Area_Image.Width, Area_Image.Height);
                CvInvoke.cvCopy(Area_Image, pad_array, IntPtr.Zero);

                pad_array.ROI = (new Rectangle(0, 0, dftSize.X, dftSize.Y));

                //Match Template
                using (Image<Gray, float> result_Matrix = pad_array.MatchTemplate(image_object, TemplateMatchingType.CcoeffNormed))
                {
                    Point[] MAX_Loc, Min_Loc;
                    double[] min, max;
                    //Limit ROI to look for Match

                    result_Matrix.ROI = new Rectangle(image_object.Width, image_object.Height, Area_Image.Width - image_object.Width, Area_Image.Height - image_object.Height);

                    result_Matrix.MinMax(out min, out max, out Min_Loc, out MAX_Loc);

                    location = new Point((MAX_Loc[0].X), (MAX_Loc[0].Y));
                    success = max[0] > max_diff;
                    var Results = result_Matrix.Convert<Gray, Double>();

                }
            }
            return success;
        }
    }
}