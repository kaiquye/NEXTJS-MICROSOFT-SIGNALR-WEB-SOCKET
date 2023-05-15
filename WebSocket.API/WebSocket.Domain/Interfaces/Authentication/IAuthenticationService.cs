namespace WebSocket.Domain.Interfaces.Authentication;

public interface IAuthenticationService<in Payload>
{
    public string generate(Payload payload);
}