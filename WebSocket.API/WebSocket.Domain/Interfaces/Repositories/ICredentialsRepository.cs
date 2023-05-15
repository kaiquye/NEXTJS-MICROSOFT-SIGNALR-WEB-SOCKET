using WebSocket.Domain.Entitys;

namespace WebSocket.Domain.Interfaces.repositories;

public interface ICredentialsRepository
{
    public Credentials Create(Credentials credentials);
}