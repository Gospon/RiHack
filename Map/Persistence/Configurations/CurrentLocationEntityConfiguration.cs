using Events.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Events.Persistence.Configurations;

public class CurrentLocationEntityConfiguration : IEntityTypeConfiguration<CurrentLocation>
{
    public void Configure(EntityTypeBuilder<CurrentLocation> builder)
    {
        builder.Property(b => b.UserId).IsRequired();
        builder.Property(b => b.CoordinateA).IsRequired();
        builder.Property(b => b.CoordinateB).IsRequired();
    }
}
