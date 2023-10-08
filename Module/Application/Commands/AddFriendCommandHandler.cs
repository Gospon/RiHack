using MediatR;
using Module.Application.Interfaces;
using Module.Application.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Application.Commands;

public record AddFriendCommand(int UserId, string FriendUsername) : IRequest<Response<string>>;

public class AddFriendCommandHandler : IRequestHandler<AddFriendCommand, Response<string>>
{
    private readonly IDbContext _context;

    public AddFriendCommandHandler(IDbContext context)
    {
        _context = context;
    }

    public async Task<Response<string>> Handle(AddFriendCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Find the user by FriendUsername
            var friendUser = _context.IdentityUser.SingleOrDefault(u => u.UserName == request.FriendUsername);

            if (friendUser == null)
            {
                return new Response<string>
                {
                    Success = false,
                    ErrorMessage = "Friend user not found."
                };
            }

            var user = _context.IdentityUser.SingleOrDefault(u => u.Id == request.UserId);

            if (user == null)
            {
                return new Response<string>
                {
                    Success = false,
                    ErrorMessage = "User not found."
                };
            }

            user.Friends.Add(friendUser);

            await _context.SaveChangesAsync(cancellationToken);

            return new Response<string>
            {
                Success = true,
                Data = "Friend added successfully."
            };
        }
        catch (Exception ex)
        {
            return new Response<string>
            {
                Success = false,
                ErrorMessage = "An error occurred while adding the friend."
            };
        }
    }
}
