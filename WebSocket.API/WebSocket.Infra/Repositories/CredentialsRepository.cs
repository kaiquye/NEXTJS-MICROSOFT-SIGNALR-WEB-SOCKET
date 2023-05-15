using WebSocket.Domain.Entitys;
using WebSocket.Domain.Interfaces.repositories;
using WebSocket.Infrastructure.Context;

namespace WebSocket.Infra.Repositories;

public class CredentialsRepository: ICredentialsRepository
{
    private readonly DbContextPg _context;

    public CredentialsRepository(DbContextPg context)
    {
        _context = context;
    }
    public Credentials Create(Credentials credentials)
    {
        _context.Credential.Add(credentials);
        return credentials;
    }
}