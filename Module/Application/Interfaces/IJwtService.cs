namespace Module.Application.Interfaces;

public interface IJwtService
{
    string GetToken(string email);
}