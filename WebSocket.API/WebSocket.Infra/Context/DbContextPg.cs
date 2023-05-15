using Microsoft.EntityFrameworkCore;
using WebSocket.Domain.Entitys;
using WebSocket.Domain.Entitys.Interface;
using WebSocket.Infrastructure.Mappings;

namespace WebSocket.Infrastructure.Context;

public class DbContextPg : DbContext
{
    public DbContextPg(DbContextOptions<DbContextPg> con) : base(con)
    { }
    
    public DbSet<Person> Person { get; set; }
    public DbSet<Credentials> Credential { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PersonMappings());
        modelBuilder.ApplyConfiguration(new CredentialsMappings());
    }

    public override int SaveChanges()
    {
        AddTimestamps();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        AddTimestamps();
        return base.SaveChangesAsync();
    }

    private void AddTimestamps()
    {
        var entities = ChangeTracker.Entries()
            .Where(x => x.Entity is EntityBase && (x.State == EntityState.Added || x.State == EntityState.Modified));

        foreach (var entity in entities)
        {
            var now = DateTime.UtcNow;

            if (entity.State == EntityState.Added)
            {
                ((EntityBase)entity.Entity).CreatedAt = now;
            }
            ((EntityBase)entity.Entity).UpdatedAt = now;
        }
    }
}