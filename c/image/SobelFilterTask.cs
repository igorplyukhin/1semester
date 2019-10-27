using System;

namespace Recognizer
{
    internal static class SobelFilterTask
    {
        public static double[,] SobelFilter(double[,] g, double[,] sx)
        {
            var sxWidth = sx.GetLength(0);
            var sxHeight = sx.GetLength(1);
            var width = g.GetLength(0);
            var height = g.GetLength(1);
            var result = new double[width, height];
            for (int x = sxWidth / 2; x < width - sxWidth / 2 ; x++)
                for (int y = sxWidth / 2; y < height - sxWidth / 2 ; y++)
                {
                    var gx = 0.0;
                    var gy = 0.0;
                    for(var xClose = 0; xClose < sxWidth; xClose++)
                        for (var yClose = 0; yClose < sxHeight; yClose++)
                        {
                            gx += g[x - sxWidth / 2 + xClose, y - sxWidth / 2 + yClose] * sx[xClose, yClose];
                            gy += g[x - sxWidth / 2 + xClose, y - sxWidth / 2 + yClose] * sx[yClose , xClose];
                        }

                     result[x, y] = Math.Sqrt(gx * gx + gy * gy);
                }
            return result;
        }
    }
}