using GameOfLife.Models;
using System.Text;

namespace GameOfLife.Services;

public sealed class OutputRenderer
{
    private readonly StringBuilder _outputBuffer;

    public OutputRenderer()
    {
        _outputBuffer = new StringBuilder();
    }

    public void RenderToConsole(GameGrid grid)
    {
        ArgumentNullException.ThrowIfNull(grid);
        
        var sortedCells = grid.GetSortedLiveCells();
        
        foreach (var cell in sortedCells)
        {
            Console.WriteLine(cell.ToString());
        }
    }

    public string RenderStatistics(GameGrid grid)
    {
        ArgumentNullException.ThrowIfNull(grid);
        
        return $"Live cells: {grid.LiveCellCount}";
    }
}