namespace RiHack.Types;

public class AuditableEntity : Entity
{
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
}
