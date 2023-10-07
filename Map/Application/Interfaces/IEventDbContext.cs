using Events.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Module.Application.Interfaces;

public interface IEventDbContext
{
    public DbSet<Event> Events { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
