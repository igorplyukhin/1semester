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
        private static int[] BestWay;

        public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
        {
            shortestDist = Double.MaxValue;
            MakePermutations(new int[checkpoints.Length], 0, checkpoints);
            return BestWay;
        }


        private static void MakePermutations(int[] permutation, int position, Point[] checkpoints)
        {
            if (position == permutation.Length)
            {
                var dist = PointExtensions.GetPathLength(checkpoints, permutation);
                if (dist < shortestDist)
                {
                    shortestDist = dist;
                    BestWay = (from elem in permutation select elem).ToArray();
                }
                
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