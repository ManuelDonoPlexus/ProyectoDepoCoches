using CarDepo.API.DTOs;

namespace CarDepo.Application.Services;

public class AuthService : IAuthRepository
{
    private readonly IAuthRepository _authrepo;

    public AuthService(IAuthRepository authrepo)
    {
        _authrepo = authrepo;
    }

    public Task<string?> Login(LoginDTO login)
    {
        return _authrepo.Login(login);
    }

    public Task Register(UserDTO user)
    {
        return _authrepo.Register(user);
    }
}