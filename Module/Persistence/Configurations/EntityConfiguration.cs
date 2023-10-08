using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Module.Domain.Entities;
using System.Reflection.Emit;

namespace Module.Persistence.Configurations;

public class EntityConfiguration : IEntityTypeConfiguration<IdentityUser>
{
    public void Configure(EntityTypeBuilder<IdentityUser> builder)
    {
        builder.Property(b => b.Id).IsRequired();
        builder.Property(b => b.Email).IsRequired();
        builder.Property(b => b.PasswordHash).IsRequired();

        builder
            .HasMany(u => u.Friends)
            .WithMany()
            .UsingEntity(j => j.ToTable("UserFriends"));
    }
}
