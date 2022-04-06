namespace JustNull
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    internal class Jigglypuff
    {
        private IntPtr window;

        public Bitmap CaptureFromScreen(Rectangle rect)
        {
            Bitmap image = !(rect == Rectangle.Empty) ? new Bitmap(rect.Width, rect.Height) : new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics graphics = Graphics.FromImage(image);
            if (rect == Rectangle.Empty)
            {
                graphics.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, image.Size, CopyPixelOperation.SourceCopy);
                return image;
            }
            graphics.CopyFromScreen(rect.X, rect.Y, 0, 0, rect.Size, CopyPixelOperation.SourceCopy);
            return image;
        }

        public bool ExpectColor(Point p, string rgb)
        {
            Color colorFromScreen = this.GetColorFromScreen(p);
            string[] strArray = rgb.Split(new char[] { '.' });
            return (((colorFromScreen.R.ToString() == strArray[0]) && (colorFromScreen.G.ToString() == strArray[1])) && (colorFromScreen.B.ToString() == strArray[2]));
        }

        public Color GetColorFromScreen(Point p)
        {
            Bitmap bitmap = this.CaptureFromScreen(new Rectangle(p, new Size(2, 2)));
            Color pixel = bitmap.GetPixel(0, 0);
            bitmap.Dispose();
            return pixel;
        }

        public void setWindow(IntPtr window)
        {
            this.window = window;
        }
    }
}