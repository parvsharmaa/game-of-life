using GameOfLife.Models;

namespace GameOfLife.Core;

public sealed class GameOfLifeEngine : IGameOfLifeEngine
{
    private int _generationCount;

    public int GenerationCount => _generationCount;

    public GameGrid EvolveGeneration(GameGrid currentGrid)
    {
        ArgumentNullException.ThrowIfNull(currentGrid);
        
        var nextGeneration = currentGrid.ComputeNextGeneration();
        _generationCount++;
        
        return nextGeneration;
    }

    public void Reset()
    {
        _generationCount = 0;
    }
}