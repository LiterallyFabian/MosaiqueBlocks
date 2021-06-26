using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace MosaiqueBlocks
{
    internal class Program
    {
        private static List<TextureItem> TextureItems;

        private static string[] BannedPatterns =
        {
            "minecraft/textures\\font",
            "water_overlay.png",
            "minecraft/textures\\colormap"
        };

        private static void Main(string[] args)
        {
            Console.Write("Path (assets/minecraft/textures):");
            string path = Console.ReadLine();
            path = path == "" ? "assets/minecraft/textures" : path;
            string[] files = Directory.GetFiles(path, "*.png", SearchOption.AllDirectories);
            TextureItems = GetTextureItems(files);

            foreach (string file in files) CreateMosaiqueBlock(file);
        }

        private static List<TextureItem> GetTextureItems(string[] files)
        {
            Console.WriteLine($"Processing {files.Length} images...");

            List<TextureItem> textures = new List<TextureItem>();
            foreach (string filePath in files)
            {
                TextureItem item = ContainsTransparent(filePath);
                if (item != null) textures.Add(item);
            }

            Console.WriteLine($"{textures.Count} images without transparancy found.");
            return textures;
        }

        /// <summary>
        /// Creates a TextureItem if image does not contain transparent areas
        /// </summary>
        private static TextureItem ContainsTransparent(string path)
        {
            Bitmap image = new Bitmap(path);
            if (image.Height != 16 || image.Height != 16) return null;
            //loop through all pixels in image and check alpha level
            for (int y = 0; y < image.Height; ++y)
            {
                for (int x = 0; x < image.Width; ++x)
                {
                    if (image.GetPixel(x, y).A != 255)
                    {
                        return null;
                    }
                }
            }
            return new TextureItem(image, path);
        }

        private static void CreateMosaiqueBlock(string path)
        {
            if (BannedPatterns.Any(path.Contains))
            {
                Console.WriteLine("skipped " + path);
                return;
            }

            Bitmap sourceImage = new Bitmap(path);
            Bitmap mosaiqueImage = new Bitmap(sourceImage.Width * 16, sourceImage.Height * 16);

            using (Graphics g = Graphics.FromImage(mosaiqueImage))
            {
                for (int y = 0; y < sourceImage.Height; ++y)
                {
                    for (int x = 0; x < sourceImage.Width; ++x)
                    {
                        Color pixel = sourceImage.GetPixel(x, y);
                        if (pixel.A > 255 / 2)
                        {
                            g.DrawImage(TextureItems[closestColor(pixel)].Bitmap, new Point(x * 16, y * 16));
                        }
                    }
                }
                string newPath = "output/" + path;
                Directory.CreateDirectory(Path.GetDirectoryName(newPath));
                mosaiqueImage.Save(newPath, ImageFormat.Png);
                Console.WriteLine($"Block {path} created.");
            }
        }

        private static int closestColor(Color target)
        {
            var colorDiffs = TextureItems.Select(n => ColorDiff(n.AverageColor, target)).Min(n => n);
            return TextureItems.FindIndex(n => ColorDiff(n.AverageColor, target) == colorDiffs);
        }

        private static int ColorDiff(Color c1, Color c2)
        {
            return (int)Math.Sqrt((c1.R - c2.R) * (c1.R - c2.R)
                                   + (c1.G - c2.G) * (c1.G - c2.G)
                                   + (c1.B - c2.B) * (c1.B - c2.B));
        }
    }
}