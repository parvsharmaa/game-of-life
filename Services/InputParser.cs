using GameOfLife.Models;
using System.Globalization;

namespace GameOfLife.Services;

public sealed class InputParser
{
    private readonly List<string> _errorMessages;
    private int _currentLineNumber;

    public InputParser()
    {
        _errorMessages = new List<string>();
    }

    public IReadOnlyList<string> ErrorMessages => _errorMessages.AsReadOnly();

    public GameGrid ParseToGameGrid(IEnumerable<string> lines)
    {
        ArgumentNullException.ThrowIfNull(lines);
        
        _errorMessages.Clear();
        _currentLineNumber = 0;
        
        var coordinates = new List<Coordinate>();
        
        foreach (var line in lines)
        {
            _currentLineNumber++;
            
            if (string.IsNullOrWhiteSpace(line))
                continue;
                
            if (TryParseLine(line.Trim(), out var coordinate))
            {
                coordinates.Add(coordinate);
            }
        }
        
        if (_errorMessages.Count > 0)
        {
            var combinedErrors = string.Join(Environment.NewLine, _errorMessages);
            throw new GameOfLifeInputException($"Input parsing failed:{Environment.NewLine}{combinedErrors}");
        }
        
        return new GameGrid(coordinates);
    }

    private bool TryParseLine(string line, out Coordinate coordinate)
    {
        coordinate = default;
        
        try
        {
            var parts = line.Split(',', StringSplitOptions.TrimEntries);
            
            if (parts.Length != 2)
            {
                AddError($"Invalid format: '{line}'. Expected 'x, y'");
                return false;
            }

            if (!int.TryParse(parts[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out var x))
            {
                AddError($"Invalid X coordinate: '{parts[0]}' in line '{line}'");
                return false;
            }

            if (!int.TryParse(parts[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out var y))
            {
                AddError($"Invalid Y coordinate: '{parts[1]}' in line '{line}'");
                return false;
            }

            coordinate = Coordinate.Create(x, y);
            return true;
        }
        catch (ArgumentOutOfRangeException ex)
        {
            AddError($"Coordinate out of range in line '{line}': {ex.Message}");
            return false;
        }
        catch (Exception ex)
        {
            AddError($"Unexpected error parsing line '{line}': {ex.Message}");
            return false;
        }
    }

    private void AddError(string message)
    {
        _errorMessages.Add($"Line {_currentLineNumber}: {message}");
    }
}