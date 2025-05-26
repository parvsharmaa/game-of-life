using GameOfLife.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife.Services;

public static class OutputRenderer
{
    public static void Render(IEnumerable<Coordinate> coords)
    {
        foreach (var c in coords.OrderBy(c => c.X).ThenBy(c => c.Y))
            Console.WriteLine($"{c.X}, {c.Y}");
    }
}
