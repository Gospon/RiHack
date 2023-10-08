using MediatR;
using Module.Persistence;
using Module.Application.Interfaces;
using Module.Application.Types;

namespace Module.Application.Commands;

public record LoginUserCommand(string Email, string Password) : IRequest<Response<string>>;
public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Response<string>>
{
    private readonly IDbContext _context;
    private readonly IJwtService _jwtService;
    public LoginUserCommandHandler(IDbContext context, IJwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }
    public async Task<Response<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = _context.IdentityUser.FirstOrDefault(u => u.Email == request.Email);
        if (user is not null)
        {
            var passwordMatch = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);


            if (passwordMatch)
            {
                var jwtToken = _jwtService.GetToken(request.Email, user.Id);
                return new Response<string>() { Success = true, Data = jwtToken };
            }
        }


        return new Response<string>() { Success = false, ErrorMessage = "Bad Credentials" };
    }
}