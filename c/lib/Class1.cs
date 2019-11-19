using System;

namespace GeometryTasks
{
    public class Vector
    {
        public double X;
        public double Y;

        public double GetLength() => Geometry.GetLength(this);

        public Vector Add(Vector secondVector) => Geometry.Add(this, secondVector);

        public bool Belongs(Segment segment) => Geometry.IsVectorInSegment(this, segment);
    }

    public class Geometry
    {
        public static double GetLength(Vector vector) =>
            Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        
        public static Vector Add(Vector a, Vector b) =>
            new Vector {X = a.X + b.X, Y = a.Y + b.Y};

        public static double GetLength(Segment segment) =>
            GetLength(new Vector {X = segment.End.X - segment.Begin.X, Y = segment.End.Y - segment.Begin.Y});

        public static bool IsVectorInSegment(Vector vector, Segment segment)
        {
            var x = vector.X;
            var y = vector.Y;
            var x1 = segment.Begin.X;
            var x2 = segment.End.X;
            var y1 = segment.Begin.Y;
            var y2 = segment.End.Y;
            return (x - x1) * (y2 - y1) == (y - y1) * (x2 - x1) && x >= x1 && x <= x2 && y >= y1 && y <= y2;
        }
    }

    public class Segment
    {
        public Vector Begin;
        public Vector End;
        
        public double GetLength() => Geometry.GetLength(this);
        
        public bool Contains(Vector vector) => Geometry.IsVectorInSegment(vector, this);
    }
}