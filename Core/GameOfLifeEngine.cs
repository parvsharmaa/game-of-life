using GameOfLife.Models;

namespace GameOfLife.Core
{
    public class GameOfLifeEngine
    {
        private static readonly (int dx, int dy)[] Directions =
        {
            (-1, -1), (-1, 0), (-1, 1),
            ( 0, -1),          ( 0, 1),
            ( 1, -1), ( 1, 0), ( 1, 1)
        };

        public HashSet<Point> Tick(HashSet<Point> liveCells)
        {
            var neighborCounts = new Dictionary<Point, int>();

            foreach (var cell in liveCells)
            {
                foreach (var (dx, dy) in Directions)
                {
                    var neighbor = cell.Offset(dx, dy);
                    if (!neighborCounts.ContainsKey(neighbor))
                        neighborCounts[neighbor] = 0;
                    neighborCounts[neighbor]++;
                }
            }

            var nextGen = new HashSet<Point>();

            foreach (var (point, count) in neighborCounts)
            {
                if (count == 3 || (count == 2 && liveCells.Contains(point)))
                {
                    nextGen.Add(point);
                }
            }

            return nextGen;
        }
    }
}
