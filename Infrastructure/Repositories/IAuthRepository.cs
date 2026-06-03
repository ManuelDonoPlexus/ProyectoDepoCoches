using CarDepo.API.DTOs;
using CarDepo.API.Models;
using CarDepo.Infrastructure.Utils;
using CarDepo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CarDepo.Infrastructure.Repositories;
// Capa de repositorio (encargada de obtener y manipular la información)

// Interfaz que señala los metodos iniciales
public interface IAuthRepository
{
    public Task Register(UserDTO user);
    public Task<String?> Login(LoginDTO login);
}

// Repositorio que implementa la interfaz de repositorio
public class AuthRepository : IAuthRepository
{
    private readonly CarDepoContext _cardepocontext; // contexto de la base de datos
    private readonly AuthUtilities _authutils; // clase de utilidades

    // Define las anteriores variables para su uso, inicializando la clase.
    public AuthRepository(CarDepoContext context, AuthUtilities utilities)
    {
        _cardepocontext = context;
        _authutils = utilities;
    }

    // Para registrar un usuario. Recibe un DTO con los datos del usuario y lo mapea a uno nuevo, encriptando la contraseña en SHA256 gracias a la clase de utilidades.
    // Tras maeparlo, lo añade a la base de datos y guarda los cambios.
    public async Task Register(UserDTO user)
    {
        var newUser = new User
        {
            Name = user.Name,
            Email = user.Email,
            Password = _authutils.EncryptToSHA256(user.Password)
        };

        await _cardepocontext.Users.AddAsync(newUser);
        await _cardepocontext.SaveChangesAsync();
    }

    // Para comprobar un login. Busca en la Base de Datos por un usuario que coincidan con el Email y la contraseña del DTO.
    // Si ambos datos coinciden, se le proporciona al usuario un Token JWT. Si no, se devuelve nulo
    public async Task<String?> Login(LoginDTO login)
    {
        var foundUser = await _cardepocontext.Users
                                .Where(u =>
                                        u.Email == login.Email &&
                                        u.Password == _authutils.EncryptToSHA256(login.Password) || u.Password == login.Password)
                                .FirstOrDefaultAsync();


        if (foundUser != null) { return _authutils.GenerateJWTToken(foundUser); }
        else { return null; }
    }
}