using Events.Domain.Interfaces;
using Events.Domain.ValueObjetcs;

namespace Events.Domain.Entities;

public class Event : AuditableEntity
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string EventDescription { get; set; }
    public Coordinate StartCoordinate { get; set; }
    public Coordinate EndCoordinate { get; set; }
    public DateTime DateTime { get; set; }
    public int? Steps { get; set; }
    public double? Kilometers { get; set; }
    public int TokenBet { get; set; }
    public string BetDescription { get; set; }
    public bool InProgress { get; set; }
}