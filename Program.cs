using GameOfLife.Core;
using GameOfLife.Services;
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        var lines = new List<string>();
        string? input;
        while (!string.IsNullOrEmpty(input = Console.ReadLine()))
            lines.Add(input);

        var initial = InputParser.ParseLines(lines);
        var engine = new GameOfLifeEngine();
        var nextGen = engine.Tick(initial);

        OutputRenderer.Render(nextGen);
    }
}
