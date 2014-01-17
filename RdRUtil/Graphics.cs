
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;

namespace RdR
{
    public static class Graphics
    {
        public static Color GetColor(string wannabeColor, Color defaultColor)
        {
            if ( Util.IsEmpty(wannabeColor) )
            {
                return defaultColor;
            }

            try
            {
                var convColor = Int32.Parse(wannabeColor.Replace("#", ""), NumberStyles.HexNumber);

                return Color.FromArgb(convColor);
            }
            catch
            {
                return defaultColor;
            }
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
    }
}