using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace MosaiqueBlocks
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Write("Path (assets/minecraft/textures):");
            string path = Console.ReadLine();
            path = path == "" ? "assets/minecraft/textures" : path;

            string[] files = Directory.GetFiles(path, "*.png", SearchOption.AllDirectories);
            Console.WriteLine($"Processing {files.Length} images...");

            List<TextureItem> textures = new List<TextureItem>();
            foreach (string filePath in files)
            {
                TextureItem item = ContainsTransparent(filePath);
                if (item != null) textures.Add(item);
            }

            Console.WriteLine($"{textures.Count} images without transparancy found.");
        }

        /// <summary>
        /// Creates a TextureItem if image does not contain transparent areas
        /// </summary>
        public static TextureItem ContainsTransparent(string path)
        {
            Bitmap image = new Bitmap(path);
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
    }
}