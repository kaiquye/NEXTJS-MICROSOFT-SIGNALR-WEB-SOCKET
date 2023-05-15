namespace WebSocket.Domain.Error;

public class ConflictException: Exception
{
    public ConflictException()
    {
    }

    public ConflictException(string message) : base(message)
    {
    }

    public ConflictException(string message, Exception data) : base(message, data)
    {
    }    
}