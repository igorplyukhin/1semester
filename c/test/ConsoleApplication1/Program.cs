namespace ReadOnlyVectorTask
{
    public class ReadOnlyVector
    {
        public readonly double X;
        public readonly double Y;

        public ReadOnlyVector(double x, double y)
        {
            X = x;
            Y = y;
        }

        public ReadOnlyVector Add(ReadOnlyVector other) => new ReadOnlyVector(X + other.X, Y + other.Y );

        public ReadOnlyVector WithX(double xNew) => new ReadOnlyVector(xNew, Y);
        
        public ReadOnlyVector WithY(double yNew) => new ReadOnlyVector(X, yNew);
    }
}