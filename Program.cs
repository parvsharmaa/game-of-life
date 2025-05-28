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

        try
        {
            var initialCells = InputParser.ParseLines(lines);
            var engine = new GameOfLifeEngine();
            var nextGeneration = engine.Tick(initialCells);
            
            OutputRenderer.Render(nextGeneration);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Environment.Exit(1);
        }
    }
}