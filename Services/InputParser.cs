using GameOfLife.Models;
using System.Globalization;

namespace GameOfLife.Services;

public sealed class InputParser : IInputParser
{
    public HashSet<Coordinate> ParseCoordinates(IEnumerable<string> lines)
    {
        ArgumentNullException.ThrowIfNull(lines);
        
        var coordinates = new HashSet<Coordinate>();
        var lineNumber = 0;
        
        foreach (var line in lines)
        {
            lineNumber++;
            
            if (string.IsNullOrWhiteSpace(line))
                continue;
                
            try
            {
                var coordinate = ParseSingleLine(line.Trim());
                coordinates.Add(coordinate);
            }
            catch (Exception ex) when (ex is not GameOfLifeInputException)
            {
                throw new GameOfLifeInputException(
                    $"Invalid format at line {lineNumber}: '{line}'. Expected format: 'x, y'", 
                    ex);
            }
        }
        
        return coordinates;
    }

    private static Coordinate ParseSingleLine(string line)
    {
        var parts = line.Split(',', StringSplitOptions.TrimEntries);
        
        if (parts.Length != 2)
        {
            throw new GameOfLifeInputException(
                $"Invalid coordinate format: '{line}'. Expected exactly two values separated by comma.");
        }

        if (!int.TryParse(parts[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out var x))
        {
            throw new GameOfLifeInputException(
                $"Invalid X coordinate: '{parts[0]}'. Must be a valid integer.");
        }

        if (!int.TryParse(parts[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out var y))
        {
            throw new GameOfLifeInputException(
                $"Invalid Y coordinate: '{parts[1]}'. Must be a valid integer.");
        }

        return Coordinate.Create(x, y);
    }
}