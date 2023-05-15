namespace WebSocket.Domain.Interfaces.Context;

public interface IUnitOfWork
{
    public void Commit();
    public void Rollback();
}