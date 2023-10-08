using Events.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Events.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private IMediator _mediator;
    public EventsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("add")]
    public async Task<ActionResult> AddEvent(AddEventCommand command)
    {
        // Add middleware for this
        var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

        command.UserId = int.Parse(userId);
        return Ok(await _mediator.Send(command));
    }
    
    [HttpGet("get-events")]
    public async Task<ActionResult> GetEvents()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var query = new GetEventsByUserIdQuery(int.Parse(userId));
        return Ok(await _mediator.Send(query));
    }
}