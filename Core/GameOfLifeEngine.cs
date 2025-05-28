using GameOfLife.Models;

namespace GameOfLife.Core;

public sealed class GameOfLifeEngine : IGameOfLifeEngine
{
    private static readonly (int dx, int dy)[] NeighborOffsets = 
    {
        (-1, -1), (-1, 0), (-1, 1),
        ( 0, -1),          ( 0, 1),
        ( 1, -1), ( 1, 0), ( 1, 1)
    };
 
    public HashSet<Coordinate> ComputeNextGeneration(HashSet<Coordinate> liveCells)
    {
        ArgumentNullException.ThrowIfNull(liveCells);
        
        var candidates = GatherCandidateCells(liveCells);
        return ApplyGameOfLifeRules(liveCells, candidates);
    }

    private HashSet<Coordinate> GatherCandidateCells(HashSet<Coordinate> liveCells)
    {
        var candidates = new HashSet<Coordinate>(liveCells.Count * 9);
        
        foreach (var cell in liveCells)
        {
            candidates.Add(cell);
        
            foreach (var (dx, dy) in NeighborOffsets)
            {
                candidates.Add(new Coordinate(cell.X + dx, cell.Y + dy));
            }
        }
        
        return candidates;
    }

    private HashSet<Coordinate> ApplyGameOfLifeRules(
        HashSet<Coordinate> liveCells, 
        HashSet<Coordinate> candidates)
    {
        var nextGeneration = new HashSet<Coordinate>();
        
        foreach (var candidate in candidates)
        {
            var isCurrentlyAlive = liveCells.Contains(candidate);
            var liveNeighborCount = CountLiveNeighbors(candidate, liveCells);
            
            if (ShouldCellBeAliveNextGeneration(isCurrentlyAlive, liveNeighborCount))
            {
                nextGeneration.Add(candidate);
            }
        }
        
        return nextGeneration;
    }

    private int CountLiveNeighbors(Coordinate cell, HashSet<Coordinate> liveCells)
    {
        var count = 0;
        
        foreach (var (dx, dy) in NeighborOffsets)
        {
            var neighbor = new Coordinate(cell.X + dx, cell.Y + dy);
            if (liveCells.Contains(neighbor))
            {
                count++;
            }
        }
        
        return count;
    }

    private static bool ShouldCellBeAliveNextGeneration(bool isCurrentlyAlive, int liveNeighborCount)
    {
        return liveNeighborCount == 3 || (isCurrentlyAlive && liveNeighborCount == 2);
    }
}