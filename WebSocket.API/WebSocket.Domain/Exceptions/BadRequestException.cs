namespace WebSocket.Domain.Error;

public class BadRequestException: Exception
{
    public BadRequestException()
    {
    }

    public BadRequestException(string message) : base(message)
    {
    }

    public BadRequestException(string message, Exception data) : base(message, data)
    {
    }
}