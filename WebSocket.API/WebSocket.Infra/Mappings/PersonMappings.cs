using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebSocket.Domain.Entitys;
namespace WebSocket.Infrastructure.Mappings;

public class PersonMappings: IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.Property(p => p.name)
            .HasMaxLength(100)
            .IsUnicode(false)
            .IsRequired(true);

        builder.Property(p => p.email)
            .IsUnicode(true)
            .IsRequired(true);
 }
}