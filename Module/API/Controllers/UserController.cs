using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Module.Application.Commands;
using Module.Application.Queries;
using System.Security.Claims;

namespace Module.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("get-users")]
    public async Task<ActionResult> GetUsers()
    {
        return Ok(await _mediator.Send(new GetUsersQuery()));
    }

    [HttpPost("add-friend")]
    public async Task<ActionResult> AddFriend(AddFriendCommand command)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var newCommand = new AddFriendCommand(int.Parse(userId), command.FriendUsername);
        return Ok(await _mediator.Send(newCommand));
    }

    [HttpGet("get-friends")]
    public async Task<ActionResult> GetUserFriendsById()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var query = new GetUserFriendsByIdQuery(int.Parse(userId));
        return Ok(await _mediator.Send(query));
    }
}
