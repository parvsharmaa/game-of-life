using GameOfLife.Models;
using System;
using System.Collections.Generic;

namespace GameOfLife.Services;

public static class InputParser
{
    public static HashSet<Coordinate> ParseLines(IEnumerable<string> lines)
    {
        var set = new HashSet<Coordinate>();
        foreach (var raw in lines)
        {
            if (string.IsNullOrWhiteSpace(raw))
                continue;

            var parts = raw.Split(',');
            if (parts.Length != 2)
                throw new FormatException($"Invalid input line: '{raw}'");

            if (!int.TryParse(parts[0].Trim(), out var x) || !int.TryParse(parts[1].Trim(), out var y))
                throw new FormatException($"Non-integer value in: '{raw}'");

            set.Add(new Coordinate(x, y));
        }
        return set;
    }
}
