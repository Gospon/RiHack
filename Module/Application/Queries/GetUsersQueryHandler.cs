using MediatR;
using Module.Application.Interfaces;
using Module.Application.Types;
using Module.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Application.Queries;

public record GetUsersQuery() : IRequest<Response<List<IdentityUser>>>;
public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, Response<List<IdentityUser>>>
{
    private readonly IDbContext _context;

    public GetUsersQueryHandler(IDbContext context)
    {
        _context = context;
    }

    public async Task<Response<List<IdentityUser>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var users = _context.IdentityUser.ToList();

            return new Response<List<IdentityUser>>
            {
                Success = true,
                Data = users
            };
        }
        catch (Exception ex)
        {
            return new Response<List<IdentityUser>>
            {
                Success = false,
                ErrorMessage = "An error occurred while fetching users."
            };
        }
    }
}