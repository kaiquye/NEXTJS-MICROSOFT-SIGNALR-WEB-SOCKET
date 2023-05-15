namespace WebSocket.Domain.Error;

public class NotFoundException : Exception
{
    public NotFoundException()
    {
    }

    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string message, Exception data) : base(message, data)
    {
    }
}