using MediatR.NotificationPublishers;
using Module.Persistence.Types;

namespace Module.Domain.Entities;

public class IdentityUser : AuditableEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Sex { get; set; }
    public int Age { get; set; }
    public double Height { get; set; }
    public double Weight { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public ICollection<IdentityUser> Friends { get; set; } = new List<IdentityUser>();
}
  