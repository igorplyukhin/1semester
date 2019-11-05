using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace RoutePlanning
{
    public static class PathFinderTask
    {
        public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
        {
            var bestWay = new int[checkpoints.Length];
            MakePermutations(new int[checkpoints.Length], 1,
                checkpoints, Double.MaxValue, bestWay, 0.0);
            return bestWay;
        }
        
        private static double MakePermutations(int[] permutation, int position,
            Point[] checkpoints, double shortestDist, int[] bestWay, double currentDist)
        {
            currentDist += PointExtensions.DistanceTo(
                checkpoints[permutation[position > 2 ? position - 2 : 0]]
                , checkpoints[permutation[position - 1]]);
            if (currentDist >= shortestDist)
                return shortestDist;

            if (position == permutation.Length)
            {
                shortestDist = currentDist;
                permutation.CopyTo(bestWay,0);
            }

            for (int i = 0; i < permutation.Length; i++)
            {
                var index = Array.IndexOf(permutation, i, 0, position);
                if (index != -1)
                    continue;
                permutation[position] = i;
                shortestDist = MakePermutations(permutation, position + 1
                    , checkpoints, shortestDist, bestWay, currentDist);
            }
            return shortestDist;
        }
    }
}