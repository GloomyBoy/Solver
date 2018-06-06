using System;
using System.Drawing;
using System.IO;

namespace EmPuzzleLogic
{
    public class Logger
    {
        public static void SaveErrorScreen(Bitmap bmp)
        {
            var name = Guid.NewGuid() + ".png";
            if (!Directory.Exists(Environment.CurrentDirectory + "\\Errors\\"))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\Errors\\");
            }
            bmp.Save(Environment.CurrentDirectory + "\\Errors\\" + name);
        }
    }
}