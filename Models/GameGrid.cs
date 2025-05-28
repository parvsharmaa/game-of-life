using System.Collections;

namespace GameOfLife.Models;

public sealed class GameGrid : IEnumerable<Coordinate>
{
    private readonly HashSet<Coordinate> _liveCells;
    private readonly HashSet<Coordinate> _candidateCache;

    public GameGrid()
    {
        _liveCells = new HashSet<Coordinate>();
        _candidateCache = new HashSet<Coordinate>();
    }

    public GameGrid(IEnumerable<Coordinate> initialLiveCells)
    {
        ArgumentNullException.ThrowIfNull(initialLiveCells);
        _liveCells = new HashSet<Coordinate>(initialLiveCells);
        _candidateCache = new HashSet<Coordinate>();
    }

    public int LiveCellCount => _liveCells.Count;

    public bool IsCellAlive(Coordinate coordinate) => _liveCells.Contains(coordinate);

    public void AddLiveCell(Coordinate coordinate)
    {
        _liveCells.Add(coordinate);
    }

    public void RemoveCell(Coordinate coordinate)
    {
        _liveCells.Remove(coordinate);
    }

    public int CountLiveNeighbors(Coordinate coordinate)
    {
        return coordinate.GetNeighbors().Count(neighbor => _liveCells.Contains(neighbor));
    }

    public HashSet<Coordinate> GetCandidateCells()
    {
        _candidateCache.Clear();
        
        foreach (var liveCell in _liveCells)
        {
            _candidateCache.Add(liveCell);
            foreach (var neighbor in liveCell.GetNeighbors())
            {
                _candidateCache.Add(neighbor);
            }
        }
        
        return new HashSet<Coordinate>(_candidateCache);
    }

    public GameGrid ComputeNextGeneration()
    {
        var nextGeneration = new GameGrid();
        var candidates = GetCandidateCells();

        foreach (var candidate in candidates)
        {
            var isCurrentlyAlive = IsCellAlive(candidate);
            var liveNeighborCount = CountLiveNeighbors(candidate);
            
            if (ShouldCellBeAliveNextGeneration(isCurrentlyAlive, liveNeighborCount))
            {
                nextGeneration.AddLiveCell(candidate);
            }
        }

        return nextGeneration;
    }

    private static bool ShouldCellBeAliveNextGeneration(bool isCurrentlyAlive, int liveNeighborCount)
    {
        return liveNeighborCount == 3 || (isCurrentlyAlive && liveNeighborCount == 2);
    }

    public Coordinate[] GetSortedLiveCells()
    {
        var cells = _liveCells.ToArray();
        Array.Sort(cells);
        return cells;
    }

    public IEnumerator<Coordinate> GetEnumerator() => _liveCells.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}