using Module.Persistence.Types;

namespace Module.Domain.Entities;

public class IdentityUser : AuditableEntity
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }
}
