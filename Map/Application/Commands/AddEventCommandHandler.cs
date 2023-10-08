using Events.Application.Types;
using Events.Domain.Entities;
using Events.Domain.ValueObjetcs;
using MediatR;
using Module.Application.Interfaces;

namespace Events.Application.Commands;

public class AddEventCommand: IRequest<Response<string>>
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string EventDesc { get; set; }
    public Coordinate StartCoordinate { get; set; }
    public Coordinate EndCoordinate { get; set; }
    public DateTime DateTime { get; set; }
    public int Steps { get; set; }
    public double Kilometers { get; set; }
    public int TokenBet { get; set; }
    public string BetDescription { get; set; }
    public bool InProgress { get; set; }
}

public class AddEventCommandHandler : IRequestHandler<AddEventCommand, Response<string>>
{
    private readonly IEventDbContext _context;
    public AddEventCommandHandler(IEventDbContext context)
    {
        _context = context;
    }

    public async Task<Response<string>> Handle(AddEventCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var newEvent = new Event
            {
                UserId = request.UserId,
                Username = request.Username,
                EventDescription = request.EventDesc,
                StartCoordinate = request.StartCoordinate,
                EndCoordinate = request.EndCoordinate,
                DateTime = request.DateTime,
                Steps = request.Steps,
                Kilometers = request.Kilometers,
                TokenBet = request.TokenBet,
                BetDescription = request.BetDescription,
                InProgress = request.InProgress,
            };

            _context.Events.Add(newEvent);

            await _context.SaveChangesAsync(cancellationToken);

            return new Response<string> { Success = true };
        }
        catch (Exception ex)
        {
            return new Response<string>
            {
                Success = false,
                ErrorMessage = "An error occurred while saving the event."
            };
        }
    }
}