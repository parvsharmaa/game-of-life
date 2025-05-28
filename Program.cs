using GameOfLife.Core;
using GameOfLife.Services;

internal static class Program
{
    private enum ExitCode
    {
        Success = 0,
        GeneralError = 1,
        InputFormatError = 2,
        CoordinateRangeError = 3
    }

    private static int Main()
    {
        try
        {
            var inputLines = ReadInputLines();
            var gameOfLifeService = CreateGameOfLifeService();
            
            gameOfLifeService.ProcessSingleGeneration(inputLines);
            
            return (int)ExitCode.Success;
        }
        catch (GameOfLifeInputException ex)
        {
            Console.Error.WriteLine($"Input Error: {ex.Message}");
            return (int)ExitCode.InputFormatError;
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.Error.WriteLine($"Coordinate Range Error: {ex.Message}");
            return (int)ExitCode.CoordinateRangeError;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected Error: {ex.Message}");
            return (int)ExitCode.GeneralError;
        }
    }

    private static List<string> ReadInputLines()
    {
        var lines = new List<string>();
        
        while (Console.ReadLine() is { } line && !string.IsNullOrEmpty(line))
        {
            lines.Add(line);
        }
        
        return lines;
    }

    private static GameOfLifeService CreateGameOfLifeService()
    {
        var engine = new GameOfLifeEngine();
        var inputParser = new InputParser();
        var outputRenderer = new OutputRenderer();
        
        return new GameOfLifeService(engine, inputParser, outputRenderer);
    }
}