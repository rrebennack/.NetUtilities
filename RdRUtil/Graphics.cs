
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace RdR
{
    public static class Graphics
    {
        public static Color GetColor(object wannabeColor, Color defaultColor)
        {
            var newColor = Graphics.HexToColor(wannabeColor);

            if ( newColor == Color.Empty )
            {
                return defaultColor;
            }

            return newColor;
        }

        public static Color HexToColor(object wannabeColor)
        {
            if ( Util.IsEmpty(wannabeColor) )
            {
                return Color.Empty;
            }

            try
            {
                var convColor = Int32.Parse(wannabeColor.ToString().Replace("#", ""), NumberStyles.HexNumber);
                var newColor = Color.FromArgb(convColor);

                return Color.FromArgb(255, newColor);
            }
            catch
            {
                return Color.Empty;
            }
        }

        public static string ColorToHex(Color wannabeHex)
        {
            if ( wannabeHex == Color.Empty )
            {
                return string.Empty;
            }

            return string.Format("#{0:X2}{1:X2}{2:X2}", wannabeHex.R, wannabeHex.G, wannabeHex.B);
        }

        public static Image GrayScale(Image sourceImage)
        {
            var imgAttr = new System.Drawing.Imaging.ImageAttributes();
            var outImage = new Bitmap(sourceImage.Width, sourceImage.Height, sourceImage.PixelFormat);

            var mGrayScale = new ColorMatrix(new float[][]
                                {
                                new float[] {0.299f, 0.299f, 0.299f, 0, 0},
                                new float[] {0.587f, 0.587f, 0.587f, 0, 0},
                                new float[] {0.114f, 0.114f, 0.114f, 0, 0},
                                new float[] {     0,      0,      0, 1, 0},
                                new float[] {     0,      0,      0, 0, 1}
                                });

            imgAttr.SetColorMatrix(mGrayScale);

            var g = System.Drawing.Graphics.FromImage(outImage);

            g.DrawImage(sourceImage, new Rectangle(0, 0, outImage.Width, outImage.Height), 0, 0, outImage.Width, outImage.Height, GraphicsUnit.Pixel, imgAttr);

            return outImage;
        }

        public static Bitmap RotateImage(Bitmap rotateMe, float angle)
        {
            float rad = (float)(angle / 180.0 * Math.PI);
            double fW = Math.Abs((Math.Cos(rad) * rotateMe.Width)) + Math.Abs((Math.Sin(rad) * rotateMe.Height));
            double fH = Math.Abs((Math.Sin(rad) * rotateMe.Width)) + Math.Abs((Math.Cos(rad) * rotateMe.Height));

            var bpOut = new Bitmap((int)Math.Ceiling(fW), (int)Math.Ceiling(fH));
            var gsTemp = System.Drawing.Graphics.FromImage(bpOut);
            var mxTranny = gsTemp.Transform;

            gsTemp.SmoothingMode = SmoothingMode.HighQuality;
            gsTemp.PixelOffsetMode = PixelOffsetMode.HighQuality;
            gsTemp.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gsTemp.TextRenderingHint = TextRenderingHint.AntiAlias;
            gsTemp.CompositingQuality = CompositingQuality.HighQuality;

            //here we do not need to translate, we rotate at the specified point
            mxTranny.RotateAt(angle, new PointF((float)(bpOut.Width / 2), (float)(bpOut.Height / 2)), MatrixOrder.Append);

            gsTemp.Transform = mxTranny;
            gsTemp.DrawImage(rotateMe, new PointF((float)((bpOut.Width - rotateMe.Width) / 2), (float)((bpOut.Height - rotateMe.Height) / 2)));
            gsTemp.Dispose();

            return bpOut;
        }
    }
}