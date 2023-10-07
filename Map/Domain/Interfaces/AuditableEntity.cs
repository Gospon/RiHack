using Events.Persistence.Interfaces;

namespace Events.Domain.Interfaces;

public class AuditableEntity : Entity
{
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
}
