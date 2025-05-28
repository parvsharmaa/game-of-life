namespace GameOfLife.Services;

public sealed class GameOfLifeInputException : Exception
{
    public GameOfLifeInputException(string message) : base(message)
    {
    }
    
    public GameOfLifeInputException(string message, Exception innerException) : base(message, innerException)
    {
    }
}