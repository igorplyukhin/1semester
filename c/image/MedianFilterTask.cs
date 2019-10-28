using System;
using System.Collections.Generic;
using System.Linq;

namespace Recognizer
{
    internal static class MedianFilterTask
    {
        static int[][] neighbours =
        {
            new[] {-1, -1}, new[] {0, -1}, new[] {1, -1},
            new[] {1, 0}, new[] {1, 1}, new[] {0, 1},
            new[] {-1, 1}, new[] {-1, 0}, new[] {0, 0}
        };

        public static double[,] MedianFilter(double[,] original)
        {
            var picWidth = original.GetLength(0);
            var picHeight = original.GetLength(1);
            var filteredPIc = new double[picWidth, picHeight];
            for (var i = 0; i < picWidth; i++)
            {
                for (var j = 0; j < picHeight; j++)
                {
                    var closePixels = GetClosePixels(i, j, picWidth, picHeight, original);
                    filteredPIc[i, j] = CalcMedianValue(closePixels, i, j);
                }
            }

            return filteredPIc;
        }

        private static List<double> GetClosePixels(int i, int j,
            int picWidth, int picHeight, double[,] original)
        {
            var closePixels = new List<double>();
            foreach (var point in neighbours)
            {
                var iNeighbor = i + point[0];
                var jNeighbor = j + point[1];
                if (IsCorrectIndex(iNeighbor, jNeighbor, picWidth, picHeight))
                    closePixels.Add(original[iNeighbor, jNeighbor]);
            }

            return closePixels;
        }

        private static double CalcMedianValue(List<double> closePixels, int i, int j)
        {
            closePixels.Sort();
            var closePixelsLen = closePixels.Count;
            if (closePixelsLen % 2 == 1)
                return closePixels[closePixelsLen / 2];
            return (closePixels[closePixelsLen / 2 - 1] + closePixels[closePixelsLen / 2]) / 2;
        }

        private static bool IsCorrectIndex(int i, int j, int width, int height)
        {
            return i >= 0 && i < width && j >= 0 && j < height;
        }
    }
}