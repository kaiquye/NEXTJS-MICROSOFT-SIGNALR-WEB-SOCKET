using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebSocket.Domain.Entitys;

namespace WebSocket.Infrastructure.Mappings;

public class CredentialsMappings: IEntityTypeConfiguration<Credentials>
{
    public void Configure(EntityTypeBuilder<Credentials> builder)
    {
        builder.Property(p => p.password_hash)
            .IsRequired(true);
        builder.Property(p => p.password_salt)
            .IsRequired(false);
        builder.HasOne<Person>(m => m.person)
            .WithOne(s => s.Credentials)
            .HasForeignKey<Credentials>(x => x.person_id)
            .HasPrincipalKey<Person>(e => e.Id);
    }
}