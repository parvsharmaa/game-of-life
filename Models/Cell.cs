namespace GameOfLife.Models;

public class Cell
{
    public Coordinate Coord { get; }
    public bool IsAlive { get; }

    public Cell(Coordinate coord, bool isAlive)
    {
        Coord = coord;
        IsAlive = isAlive;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null || obj is not Cell other)
            return false;
            
        return Coord.Equals(other.Coord);
    }

    public override int GetHashCode()
    {
        return Coord.GetHashCode();
    }
}