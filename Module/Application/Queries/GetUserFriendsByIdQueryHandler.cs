using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Application.Interfaces;
using Module.Application.Types;
using Module.Domain.Entities;

public record GetUserFriendsByIdQuery(int UserId) : IRequest<Response<List<IdentityUser>>>;

public class GetUserFriendsByIdQueryHandler : IRequestHandler<GetUserFriendsByIdQuery, Response<List<IdentityUser>>>
{
    private readonly IDbContext _context;

    public GetUserFriendsByIdQueryHandler(IDbContext context)
    {
        _context = context;
    }

    public async Task<Response<List<IdentityUser>>> Handle(GetUserFriendsByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var user = _context.IdentityUser
                .Include(u => u.Friends)
                .SingleOrDefault(u => u.Id == request.UserId);

            if (user != null)
            {
                var friends = user.Friends.ToList();

                return new Response<List<IdentityUser>>
                {
                    Success = true,
                    Data = friends
                };
            }
            else
            {
                return new Response<List<IdentityUser>>
                {
                    Success = false,
                    ErrorMessage = "User not found."
                };
            }
        }
        catch (Exception ex)
        {
            return new Response<List<IdentityUser>>
            {
                Success = false,
                ErrorMessage = "An error occurred while fetching the user's friends."
            };
        }
    }
}