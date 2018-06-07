using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;

namespace EmPuzzleLogic.Helper
{
    public static class ProcessHelper
    {
        public class User32
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

        public static Bitmap CaptureProcessWindow(string procName, out User32.Rect formRect)
        {
            formRect = new User32.Rect();
            var procs = Process.GetProcessesByName(procName);
            if (procs.Length == 0)
                return null;
            var rect = new User32.Rect();
            var proc = procs.Where(p =>
            {
                User32.GetWindowRect(p.MainWindowHandle, ref rect);
                return rect.left != 0 && rect.right != 0;
            }).First();


            int width = rect.right - rect.left;
            int height = rect.bottom - rect.top;

            var bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            Graphics graphics = Graphics.FromImage(bmp);
            graphics.CopyFromScreen(rect.left, rect.top, 0, 0, new Size(width, height), CopyPixelOperation.SourceCopy);
            formRect = rect;
            return bmp;
        }
    }
}