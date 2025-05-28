using System;

namespace GameOfLife.Models;

public readonly record struct Coordinate(int X, int Y) : IComparable<Coordinate>
{
    public static void ValidateBounds(int x, int y)
    {
        const int maxValue = 100_000;
        const int minValue = -100_000;
        
        if (x < minValue || x > maxValue || y < minValue || y > maxValue)
        {
            throw new ArgumentOutOfRangeException(
                nameof(Coordinate),
                $"Coordinate ({x}, {y}) is outside valid range [{minValue}, {maxValue}]");
        }
    }

    public static Coordinate Create(int x, int y)
    {
        ValidateBounds(x, y);
        return new Coordinate(x, y);
    }

    public int CompareTo(Coordinate other)
    {
        var xComparison = X.CompareTo(other.X);
        return xComparison != 0 ? xComparison : Y.CompareTo(other.Y);
    }

    public override string ToString() => $"{X}, {Y}";
}