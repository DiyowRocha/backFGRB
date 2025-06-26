using backFGRB.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backFGRB.Infrastructure.Context.Configurations;

public class LogConfiguration : IEntityTypeConfiguration<Log>
{
    public void Configure(EntityTypeBuilder<Log> builder)
    {
        builder.HasKey(l => l.Id);

        builder.Property(l => l.Timestamp).IsRequired();
        builder.Property(l => l.Action).IsRequired().HasMaxLength(100);
        builder.Property(l => l.Resource).IsRequired().HasMaxLength(100);
        builder.Property(l => l.PerformedBy).IsRequired().HasMaxLength(100);

        builder.Property(l => l.DataBefore).HasColumnType("text");
        builder.Property(l => l.DataAfter).HasColumnType("text");
        
        builder.Property(l => l.IPAddress).HasMaxLength(45);
    }
}