using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace RoutePlanning
{
    public static class PathFinderTask
    {
        public static int[] FindCheckpointsOrder(Point[] checkpoints)
        {
            var way = new int[checkpoints.Length];
            way[0] = 0;
            var min = double.MaxValue;
            var minindex = 0;
            for (var i = 1; i < checkpoints.Length; i++)
            {
                for (var j = 1; j < checkpoints.Length; j++)
                {
                    if (Array.IndexOf(way,j) != -1)
                        continue;
                    var dist = PointExtensions.DistanceTo(checkpoints[i - 1], checkpoints[j]);
                    if (dist < min)
                    {
                        min = dist;
                        minindex = j;
                    }
                    
                }

                way[i] = minindex;
            }

            return way;

        }

        private static double CalcDistance(Point a, Point b)
        {
            return Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));
        }
        
        
        
    }
}