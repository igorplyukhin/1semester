using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    public class Program
    {
        public class ClockwiseComparer : IComparer
        {
            public int Compare(object x, object y)
            {
                double NormalizeAngle(Point p)
                {
                    var angle = Math.Atan2(p.Y, p.X);
                    if (angle < 0)
                        angle += Math.PI * 2;
                    return angle;
                }

                var p1 = (Point) x;
                var p2 = (Point) y;
                return NormalizeAngle(p1).CompareTo(NormalizeAngle(p2));
            }
        }

        public class Point
        {
            public double X;
            public double Y;
        }

        public static void Main()
        {
            Console.WriteLine(Math.Atan2(0, -1));
        }
    }
}