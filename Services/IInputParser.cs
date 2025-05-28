using GameOfLife.Models;

namespace GameOfLife.Services;

public interface IInputParser
{
    HashSet<Coordinate> ParseCoordinates(IEnumerable<string> lines);
}