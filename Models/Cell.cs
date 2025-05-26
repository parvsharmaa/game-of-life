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
}
