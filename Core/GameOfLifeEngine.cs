using GameOfLife.Models;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife.Core
{
    public class GameOfLifeEngine
    {
        private readonly (int dx, int dy)[] _neighbors = 
        {
            (-1, -1), (-1, 0), (-1, 1),
            ( 0, -1),          ( 0, 1),
            ( 1, -1), ( 1, 0), ( 1, 1)
        };

        public HashSet<Cell> Tick(HashSet<Cell> currentGeneration)
        {
            var candidateCells = GatherCandidateCells(currentGeneration);
            var nextGeneration = ComputeNextGeneration(candidateCells);
            return nextGeneration;
        }

        private HashSet<Cell> GatherCandidateCells(HashSet<Cell> liveCells)
        {
            var candidates = new HashSet<Cell>(liveCells);
            
            foreach (var cell in liveCells)
            {
                foreach (var (dx, dy) in _neighbors)
                {
                    var neighborCoord = new Coordinate(cell.Coord.X + dx, cell.Coord.Y + dy);
                    candidates.Add(new Cell(neighborCoord, false));
                }
            }
            
            return candidates;
        }

        private HashSet<Cell> ComputeNextGeneration(HashSet<Cell> candidateCells)
        {
            var nextGeneration = new HashSet<Cell>();
            
            foreach (var cell in candidateCells)
            {
                var liveNeighborCount = CountLiveNeighbors(cell.Coord, candidateCells);
                var shouldLive = ShouldCellLive(cell.IsAlive, liveNeighborCount);
                
                if (shouldLive)
                {
                    nextGeneration.Add(new Cell(cell.Coord, true));
                }
            }
            
            return nextGeneration;
        }

        private int CountLiveNeighbors(Coordinate coord, HashSet<Cell> cells)
        {
            return _neighbors
                .Select(n => new Coordinate(coord.X + n.dx, coord.Y + n.dy))
                .Count(neighborCoord => 
                    cells.Any(c => c.Coord.Equals(neighborCoord) && c.IsAlive));
        }

        private bool ShouldCellLive(bool isAlive, int liveNeighborCount)
        {
            return liveNeighborCount == 3 || (isAlive && liveNeighborCount == 2);
        }
    }
}