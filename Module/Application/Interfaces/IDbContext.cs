using Microsoft.EntityFrameworkCore;
using Module.Domain.Entities;

namespace Module.Application.Interfaces;

public interface IDbContext
{
    public DbSet<IdentityUser> IdentityUser { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
