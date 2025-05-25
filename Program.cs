using GameOfLife.Core;
using GameOfLife.Models;

class Program
{
    static void Main()
    {
        var liveCells = new HashSet<Point>();

        string? line;
        while (!string.IsNullOrWhiteSpace(line = Console.ReadLine()))
        {
            var parts = line.Split(',');
            int x = int.Parse(parts[0].Trim());
            int y = int.Parse(parts[1].Trim());
            liveCells.Add(new Point(x, y));
        }

        var engine = new GameOfLifeEngine();
        var nextGen = engine.Tick(liveCells);

        foreach (var cell in nextGen.OrderBy(p => p.X).ThenBy(p => p.Y))
        {
            Console.WriteLine(cell);
        }
    }
}
