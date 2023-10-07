using Events.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Events.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    private IMediator _mediator;
    public EventsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("add")]
    public async Task<ActionResult> Register(AddEventCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
}