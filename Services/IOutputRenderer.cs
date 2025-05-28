using GameOfLife.Models;

namespace GameOfLife.Services;

public interface IOutputRenderer
{
    void RenderCoordinates(HashSet<Coordinate> coordinates);
}