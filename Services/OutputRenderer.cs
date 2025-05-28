using GameOfLife.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife.Services
{
    public static class OutputRenderer
    {
        public static void Render(IEnumerable<Cell> cells)
        {
            var aliveCells = cells
                .Where(c => c.IsAlive)
                .OrderBy(c => c.Coord.X)
                .ThenBy(c => c.Coord.Y);
                
            foreach (var cell in aliveCells)
            {
                Console.WriteLine($"{cell.Coord.X}, {cell.Coord.Y}");
            }
        }
    }
}