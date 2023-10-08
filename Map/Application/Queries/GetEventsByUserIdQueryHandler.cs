using Events.Application.Types;
using Events.Domain.Entities;
using MediatR;
using Module.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public record GetEventsByUserIdQuery(int UserId) : IRequest<Response<List<Event>>>;

public class GetEventsByUserIdQueryHandler : IRequestHandler<GetEventsByUserIdQuery, Response<List<Event>>>
{
    private readonly IEventDbContext _context;

    public GetEventsByUserIdQueryHandler(IEventDbContext context)
    {
        _context = context;
    }

    public async Task<Response<List<Event>>> Handle(GetEventsByUserIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var events = _context.Events
                .Where(e => e.UserId == request.UserId)
                .ToList();

            return new Response<List<Event>>
            {
                Success = true,
                Data = events
            };
        }
        catch (Exception ex)
        {
            return new Response<List<Event>>
            {
                Success = false,
                ErrorMessage = "An error occurred while fetching events."
            };
        }
    }
}
