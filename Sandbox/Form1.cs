using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Sandbox
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoadSrc_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                pbSRC.Image = Image.FromFile(dlg.FileName);
            }
        }

        private void btnLoadTemplate_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                pbTemplate.Image = Image.FromFile(dlg.FileName);
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            Image<Gray, byte> srcGrayS = new Image<Gray, byte>((Bitmap) pbSRC.Image);
            var resultS = srcGrayS.Sobel(1, 0, 3);
            //pbSRC.Image = resultS.Bitmap;

            Image<Gray, byte> srcGrayT = new Image<Gray, byte>((Bitmap)pbTemplate.Image);
            var resultT = srcGrayT.Sobel(1, 0, 3);
            //pbTemplate.Image = resultT.Bitmap;

            var result = resultS.MatchTemplate(resultT, TemplateMatchingType.Ccoeff);
            result.MinMax(out double[] minV, out double[] maxV, out Point[] minL, out Point[] maxL);
            Rectangle match = new Rectangle(maxL[0], srcGrayT.Size);

            Bitmap bmp = (Bitmap)pbSRC.Image.Clone();
            var g = Graphics.FromImage(bmp);// CreateGraphics();
            Brush b = new SolidBrush(Color.Blue);
            g.FillRectangle(b, match);
            g.Save();
            lbWeight.Items.Add($"MAX: {maxV[0]}");
            pbSRC.Image = bmp;
            //pbSRC.Image = srcGrayS.Bitmap;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
