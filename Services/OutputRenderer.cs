using GameOfLife.Models;

namespace GameOfLife.Services;

public sealed class OutputRenderer : IOutputRenderer
{
    public void RenderCoordinates(HashSet<Coordinate> coordinates)
    {
        ArgumentNullException.ThrowIfNull(coordinates);
        
        var sortedCoordinates = coordinates.ToArray();
        Array.Sort(sortedCoordinates);
        
        foreach (var coordinate in sortedCoordinates)
        {
            Console.WriteLine(coordinate.ToString());
        }
    }
}