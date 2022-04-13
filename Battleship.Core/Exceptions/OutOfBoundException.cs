namespace Battleship.Core.Exceptions;

public class OutOfBoundException : Exception
{
    public OutOfBoundException()
    {
    }
    
    public OutOfBoundException(string message) : base(message)
    {
    }
    
    public OutOfBoundException(string message, Exception inner) : base(message, inner)
    {
    }

    public static OutOfBoundException New()
    {
        return new OutOfBoundException();
    }
}