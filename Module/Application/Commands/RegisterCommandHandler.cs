using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Application.Interfaces;
using Module.Application.Types;
using Module.Domain.Entities;

namespace Module.Application.Commands;

public record RegisterUserCommand(string FirstName, string LastName, string Password, string Email) : IRequest<Response<string>>;
public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Response<string>>
{
    private readonly IDbContext _context;
    public readonly IJwtService _jwtService;
    public RegisterUserCommandHandler(
        IDbContext context,
        IJwtService jwtService
        )
    {
        _context = context;
        _jwtService = jwtService;
    }
    public async Task<Response<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        bool userExists = await _context.IdentityUser.AnyAsync(u => u.Email == request.Email);
        if (userExists)
        {
            return new Response<string>() { Success = false, ErrorMessage = "User already exists." };
        }
        else
        {
            var identityUser = new IdentityUser()
            {
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            await _context.IdentityUser.AddAsync(identityUser);
            await _context.SaveChangesAsync(cancellationToken);

            var jwtToken = _jwtService.GetToken(request.Email);

            return new Response<string>() { Success = true, Data = jwtToken };
        }
    }
}
