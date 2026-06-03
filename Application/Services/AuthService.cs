using CarDepo.API.DTOs;
using CarDepo.Infrastructure.Repositories;

namespace CarDepo.Application.Services;
// Capa de servicioo (para comunicar los controladores y los repositorios)

// Clase del servicio, que implementa el repositorio por sus metodos.
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