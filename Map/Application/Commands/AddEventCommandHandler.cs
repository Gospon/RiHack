using Events.Application.Types;
using Events.Domain.Entities;
using MediatR;
using Module.Application.Interfaces;

namespace Events.Application.Commands;

public record AddEventCommand(
    int UserId,
    string Username,
    string EventDesc,
    double LatitudeA,
    double LongitudeA,
    double LatitudeB,
    double LongitudeB,
    string Date,
    string Time,
    int Steps,
    double Kilometers
) : IRequest<Response<string>>;
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
                LatitudeA = request.LatitudeA,
                LongitudeA = request.LatitudeA,
                LongitudeB = request.LongitudeB,
                LatitudeB = request.LatitudeB,
                Date = request.Date,
                Time = request.Time,
                Steps = request.Steps,
                Kilometers = request.Kilometers
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