using System;
using System.Collections.Generic;
using System.Linq;

namespace Recognizer
{
    internal static class MedianFilterTask
    {
        public static double[,] MedianFilter(double[,] original)
        {
            var picWidth = original.GetLength(0);
            var picHeight = original.GetLength(1);
            var filteredPIc = new double[picWidth, picHeight];
            var closePixels = new List<double>();
            for (var i = 0; i < picWidth; i++)
            {
                for (var j = 0; j < picHeight; j++)
                {
                    closePixels = GetSortedClosePixelsValues(i, j, closePixels, picWidth, picHeight, original);
                    var closePixelsLen = closePixels.Count;
                    filteredPIc[i, j] = closePixelsLen % 2 == 1
                        ? closePixels.ElementAt(closePixelsLen / 2)
                        : (closePixels.ElementAt(closePixelsLen / 2 - 1)
                           + closePixels.ElementAt(closePixelsLen / 2)) / 2;
                }
            }

            return filteredPIc;
        }

        private static List<double> GetSortedClosePixelsValues(int i, int j, List<double> closePixels,
            int picWidth, int picHeight, double[,] original)
        {
            int[][] points =
            {
                new[] {-1, -1}, new[] {0, -1}, new[] {1, -1},
                new[] {1, 0}, new[] {1, 1}, new[] {0, 1},
                new[] {-1, 1}, new[] {-1, 0}, new[] {0, 0}
            };
            closePixels.Clear();
            foreach (var point in points)
            {
                var iNeighbor = i + point[0];
                var jNeighbor = j + point[1];
                if (iNeighbor >= 0 && iNeighbor < picWidth && jNeighbor >= 0 && jNeighbor < picHeight)
                    closePixels.Add(original[iNeighbor, jNeighbor]);
            }

            closePixels.Sort();
            return closePixels;
        }
    }
}