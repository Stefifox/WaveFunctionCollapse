using System;
using System.Drawing;

namespace WaveFunctionCollapse
{
    public class Extras
    {
        private static int _pixelSizeX = 800;
        private static int _pixelSizey = 600;

        public static void PrintGrid(Node[,] grid, int width, int height)
        {
            using var bitmap = new Bitmap(width * _pixelSizeX, height * _pixelSizey);
            using var graphics = Graphics.FromImage(bitmap);
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var current = grid[x, y];
                    var image = Image.FromFile($"./Assets/{current.Texture}");

                    graphics.DrawImage(image, current.x * _pixelSizeX, current.y * _pixelSizey);
                }
            }

            bitmap.Save($"./out/output{DateTime.Now.ToString("hhmmss")}.png");
        }
    }
}

