using System;
using System.Drawing;

namespace WaveFunctionCollapse
{
    public class Extras
    {
        private static int _pixelSizeX = 800;
        private static int _pixelSizey = 600;

        public static void PrintGrid(Node[,] grid, int rows, int columns)
        {
            using var bitmap = new Bitmap(columns * _pixelSizeX, rows * _pixelSizey);
            using var graphics = Graphics.FromImage(bitmap);
            for (var row = 0; row < rows; row++)
            {
                for (var col = 0; col < columns; col++)
                {
                    var current = grid[row, col];
                    var image = Image.FromFile($"./Assets/{current.Texture}");

                    graphics.DrawImage(image,current.col * _pixelSizeX, current.row * _pixelSizey);
                }
            }

            bitmap.Save($"./out/output{DateTime.Now.ToString("hhmmss")}.png");
        }
    }
}

