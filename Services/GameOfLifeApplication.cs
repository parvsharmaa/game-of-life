using GameOfLife.Core;
using GameOfLife.Models;

namespace GameOfLife.Services;

public sealed class GameOfLifeApplication
{
    private readonly IGameOfLifeEngine _engine;
    private readonly InputParser _inputParser;
    private readonly OutputRenderer _outputRenderer;
    private GameGrid? _currentGrid;

    public GameOfLifeApplication(IGameOfLifeEngine engine)
    {
        _engine = engine ?? throw new ArgumentNullException(nameof(engine));
        _inputParser = new InputParser();
        _outputRenderer = new OutputRenderer();
    }

    public GameGrid? CurrentGrid => _currentGrid;

    public void LoadInitialState(IEnumerable<string> inputLines)
    {
        ArgumentNullException.ThrowIfNull(inputLines);
        
        _currentGrid = _inputParser.ParseToGameGrid(inputLines);
    }

    public void EvolveOneGeneration()
    {
        if (_currentGrid == null)
            throw new InvalidOperationException("No initial state loaded. Call LoadInitialState first.");
            
        _currentGrid = _engine.EvolveGeneration(_currentGrid);
    }

    public void RenderCurrentState()
    {
        if (_currentGrid == null)
            throw new InvalidOperationException("No grid state available to render.");
            
        _outputRenderer.RenderToConsole(_currentGrid);
    }

    public void ProcessSingleGeneration(IEnumerable<string> inputLines)
    {
        LoadInitialState(inputLines);
        EvolveOneGeneration();
        RenderCurrentState();
    }

    public string GetCurrentStatistics()
    {
        if (_currentGrid == null)
            return "No grid loaded";
            
        return _outputRenderer.RenderStatistics(_currentGrid);
    }
}