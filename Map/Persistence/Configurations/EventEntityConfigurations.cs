using Events.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Events.Persistence.Configurations;

public class EventEntityConfigurations : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.Property(b => b.Id).IsRequired();
        builder.Property(b => b.Username).IsRequired();
        builder.Property(b => b.EventDescription).IsRequired();
        builder.Property(b => b.Steps).IsRequired();
    }
}
