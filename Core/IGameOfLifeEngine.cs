using GameOfLife.Models;

namespace GameOfLife.Core;

public interface IGameOfLifeEngine
{
    HashSet<Coordinate> ComputeNextGeneration(HashSet<Coordinate> liveCells);
}