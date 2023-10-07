using Events.Domain.Entities;
using Events.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Module.Application.Interfaces;

namespace Events.Persistence;

public class EventsDbContext : DbContext, IEventDbContext
{
    public EventsDbContext(DbContextOptions<EventsDbContext> options) : base(options) { }
    public DbSet<Event> Events { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new EventEntityConfigurations().Configure(modelBuilder.Entity<Event>());
    }

}
