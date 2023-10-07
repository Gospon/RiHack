using Microsoft.EntityFrameworkCore;
using Module.Application.Interfaces;
using Module.Domain.Entities;
using Module.Persistence.Configurations;

namespace Module.Persistence;

public class ModuleDbContext : DbContext, IDbContext
{
    public ModuleDbContext(DbContextOptions<ModuleDbContext> options) : base(options) { }

    public DbSet<IdentityUser> IdentityUser { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new EntityConfiguration().Configure(modelBuilder.Entity<IdentityUser>());
    }
}
