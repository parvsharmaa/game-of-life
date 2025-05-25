namespace GameOfLife.Models
{
    public class Point
    {
        public int X { get; }
        public int Y { get; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point Offset(int dx, int dy)
        {
            return new Point(X + dx, Y + dy);
        }

        public override bool Equals(object? obj)
        {
            return obj is Point p && p.X == X && p.Y == Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public override string ToString()
        {
            return $"{X}, {Y}";
        }
    }
}
