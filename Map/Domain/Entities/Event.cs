using Events.Domain.Interfaces;

namespace Events.Domain.Entities;

public class Event : AuditableEntity
{
    public int UserId { get; set; }    
    public string Username { get; set; }
    public string EventDescription { get; set; }
    public double LatitudeA { get; set; }
    public double LongitudeA { get; set; }
    public double LatitudeB { get; set; }
    public double LongitudeB { get; set; }
    public string Date { get; set; }
    public string Time { get; set; }
    public int Steps { get; set; }
    public double Kilometers { get; set; }
}
