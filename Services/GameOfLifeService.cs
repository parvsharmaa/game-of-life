using GameOfLife.Core;
using GameOfLife.Models;

namespace GameOfLife.Services;

public sealed class GameOfLifeService
{
    private readonly IGameOfLifeEngine _engine;
    private readonly IInputParser _inputParser;
    private readonly IOutputRenderer _outputRenderer;

    public GameOfLifeService(
        IGameOfLifeEngine engine, 
        IInputParser inputParser, 
        IOutputRenderer outputRenderer)
    {
        _engine = engine ?? throw new ArgumentNullException(nameof(engine));
        _inputParser = inputParser ?? throw new ArgumentNullException(nameof(inputParser));
        _outputRenderer = outputRenderer ?? throw new ArgumentNullException(nameof(outputRenderer));
    }

    public void ProcessSingleGeneration(IEnumerable<string> inputLines)
    {
        ArgumentNullException.ThrowIfNull(inputLines);
        
        var initialCells = _inputParser.ParseCoordinates(inputLines);
        var nextGeneration = _engine.ComputeNextGeneration(initialCells);
        
        _outputRenderer.RenderCoordinates(nextGeneration);
    }
}