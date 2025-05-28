using GameOfLife.Models;

namespace GameOfLife.Core;

public interface IGameOfLifeEngine
{
    GameGrid EvolveGeneration(GameGrid currentGrid);
}