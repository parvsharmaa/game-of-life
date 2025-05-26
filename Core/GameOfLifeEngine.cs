using GameOfLife.Models;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife.Core;

public class GameOfLifeEngine
{
    private static readonly (int dx, int dy)[] Neighbors =
    {
        (-1, -1), (-1, 0), (-1, 1),
        ( 0, -1),          ( 0, 1),
        ( 1, -1), ( 1, 0), ( 1, 1)
    };

    public HashSet<Coordinate> Tick(HashSet<Coordinate> currentGen)
    {
        var candidates = GatherCandidates(currentGen);
        var neighborCounts = CountNeighbors(currentGen, candidates);
        return ComputeNextGeneration(currentGen, neighborCounts);
    }

    private static HashSet<Coordinate> GatherCandidates(HashSet<Coordinate> live)
    {
        var all = new HashSet<Coordinate>(live);
        foreach (var coord in live)
        {
            foreach (var (dx, dy) in Neighbors)
                all.Add(new Coordinate(coord.X + dx, coord.Y + dy));
        }
        return all;
    }

    private static Dictionary<Coordinate, int> CountNeighbors(HashSet<Coordinate> live, HashSet<Coordinate> candidates)
    {
        var counts = candidates.ToDictionary(c => c, _ => 0);
        foreach (var coord in live)
        {
            foreach (var (dx, dy) in Neighbors)
            {
                var neighbor = new Coordinate(coord.X + dx, coord.Y + dy);
                if (counts.ContainsKey(neighbor))
                    counts[neighbor]++;
            }
        }
        return counts;
    }

    private static HashSet<Coordinate> ComputeNextGeneration(HashSet<Coordinate> live, Dictionary<Coordinate, int> counts)
    {
        var next = new HashSet<Coordinate>();
        foreach (var (coord, count) in counts)
        {
            if (ShouldLive(live.Contains(coord), count))
                next.Add(coord);
        }
        return next;
    }

    private static bool ShouldLive(bool isAlive, int neighbors)
        => neighbors == 3 || (isAlive && neighbors == 2);
}
