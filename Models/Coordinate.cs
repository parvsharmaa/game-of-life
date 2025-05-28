using System;

namespace GameOfLife.Models;

public readonly record struct Coordinate(int X, int Y) : IComparable<Coordinate>
{
    private const int MaxCoordinateValue = 100_000;
    private const int MinCoordinateValue = -100_000;

    public static Coordinate Create(int x, int y)
    {
        if (x < MinCoordinateValue || x > MaxCoordinateValue || 
            y < MinCoordinateValue || y > MaxCoordinateValue)
        {
            throw new ArgumentOutOfRangeException(
                nameof(Coordinate),
                $"Coordinate ({x}, {y}) is outside valid range [{MinCoordinateValue}, {MaxCoordinateValue}]");
        }
        return new Coordinate(x, y);
    }

    public IEnumerable<Coordinate> GetNeighbors()
    {
        yield return new Coordinate(X - 1, Y - 1);
        yield return new Coordinate(X - 1, Y);
        yield return new Coordinate(X - 1, Y + 1);
        yield return new Coordinate(X, Y - 1);
        yield return new Coordinate(X, Y + 1);
        yield return new Coordinate(X + 1, Y - 1);
        yield return new Coordinate(X + 1, Y);
        yield return new Coordinate(X + 1, Y + 1);
    }

    public int CompareTo(Coordinate other)
    {
        var xComparison = X.CompareTo(other.X);
        return xComparison != 0 ? xComparison : Y.CompareTo(other.Y);
    }

    public override string ToString() => $"{X}, {Y}";
}