using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace RoutePlanning
{
    public static class PathFinderTask
    {
        private static double shortestDist = Double.MaxValue;
        private static int[] bestWay;

        public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
        {
            shortestDist = Double.MaxValue;
            MakePermutations(new int[checkpoints.Length], 1, checkpoints);
            return bestWay;
        }


        private static void MakePermutations(int[] permutation, int position, Point[] checkpoints)
        {
            var currentDist = PointExtensions.GetPathLength(checkpoints, permutation.Take(position).ToArray());
            if (currentDist >= shortestDist)
                return;

            if (position == permutation.Length)
            {
                shortestDist = currentDist;
                bestWay = permutation.ToArray();
                return;
            }

            for (int i = 0; i < permutation.Length; i++)
            {
                var index = Array.IndexOf(permutation, i, 0, position);
                if (index != -1)
                    continue;
                permutation[position] = i;
                MakePermutations(permutation, position + 1, checkpoints);
            }
        }
    }
}