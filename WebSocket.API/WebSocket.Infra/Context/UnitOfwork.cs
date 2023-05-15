using WebSocket.Domain.Interfaces.Context;

namespace WebSocket.Infrastructure.Context;

public class UnitOfWork
{
    private readonly DbContextPg _dbContextPg;
    public UnitOfWork(DbContextPg dbContext)
    {
        _dbContextPg = dbContext;
    }
    public void Commit()
    {
       _dbContextPg.SaveChanges();
    }

    public void Rollback()
    {
        Console.WriteLine("[rollback action started]");
        // rollback action
    }
}