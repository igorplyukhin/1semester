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
            var sy = TransposeMatrix(sx);
            for (var x = sxWidth / 2; x < width - sxWidth / 2; x++)
            for (var y = sxWidth / 2; y < height - sxHeight / 2; y++)
            {
                var gxgy = CalcGradients(x, y, sx, sy, g);
                result[x, y] = Math.Sqrt(gxgy.Item1 * gxgy.Item1 + gxgy.Item2 * gxgy.Item2);
            }

            return result;
        }

        private static double[,] TransposeMatrix(double[,] original)
        {
            var width = original.GetLength(0);
            var height = original.GetLength(1);
            var transposedMatrix = new double[width, height];
            for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
            {
                transposedMatrix[x, y] = original[y, x];
            }

            return transposedMatrix;
        }

        private static Tuple<double,double> CalcGradients(int x, int y, double[,] sx, double[,] sy, double[,] g)
        {
            var sxWidth = sx.GetLength(0);
            var sxHeight = sx.GetLength(1);
            var gx = 0.0;
            var gy = 0.0;
            for (var xClose = 0; xClose < sxWidth; xClose++)
            for (var yClose = 0; yClose < sxHeight; yClose++)
            {
                gx += g[x - sxWidth / 2 + xClose, y - sxWidth / 2 + yClose] * sx[xClose, yClose];
                gy += g[x - sxWidth / 2 + xClose, y - sxWidth / 2 + yClose] * sy[xClose, yClose];
            }

            return Tuple.Create(gx, gy);
        }
    }
}