using System;
using System.Drawing;

namespace MosaiqueBlocks
{
    internal class TextureItem
    {
        public string Path;
        public Bitmap Bitmap;
        public Color AverageColor;

        public TextureItem(Bitmap image, string path)
        {
            Bitmap = image;
            Path = path;
            AverageColor = CalculateAverageColor(image);

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"TextureItem {path} created.");
            Console.ResetColor();
        }

        private static Color CalculateAverageColor(Bitmap bmp)
        {
            //source: https://stackoverflow.com/a/1068399/11420970
            int r = 0;
            int g = 0;
            int b = 0;

            int total = 0;

            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color clr = bmp.GetPixel(x, y);

                    r += clr.R;
                    g += clr.G;
                    b += clr.B;

                    total++;
                }
            }

            //Calculate average
            r /= total;
            g /= total;
            b /= total;

            return Color.FromArgb(r, g, b);
        }
    }
}