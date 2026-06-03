using Microsoft.AspNetCore.Mvc;
using CarDepo.API.DTOs;
using Microsoft.AspNetCore.Authorization;
using CarDepo.Application.Services;
// Capa de controlador. La parte de la aplicación sobre la que se haran las peticiones

namespace CarDepo.API.Controllers;
// Clase de Controlador, que implementa la clase base 'ControllerBase'

[Route("api/[controller]")] // Indica la ruta del controlador
[ApiController] // Para indicar que es un controlador que responde a las llamadas de la API
[AllowAnonymous] // Para indicar que los metodos del controlador pueden usarse sin autenticación 
public class AuthController : ControllerBase
{
    private readonly AuthService _authservice; // servicio con el que interactua el controlador

    public AuthController(AuthService authService)
    {
        _authservice = authService;
    }

    // Para registrar a un usuario. Recibe como parametro un DTO del usuario 
    // Accede al servicio que se ocupa de esta acción, y devuelve una respuesta 200 OK segun la respuesta del registro
    [HttpPost] // Responde al metodo POST
    [Route("register")] // La ruta del metodo
    public async Task<IActionResult> Register(UserDTO user)
    {
        return Ok(_authservice.Register(user));
    }

    // Para iniciar sesión como usuario. Recibe un DTO de Login como parametro
    // Si el servicio no responde con nulos, se devuelve el token resultante
    [HttpPost] // Responde al metodo POST
    [Route("login")] // La ruta del metodo
    public async Task<String?> Login(LoginDTO login)
    {
        String? token = await _authservice.Login(login);
        if (token != null) { return token; }
        else { return null; }

    }
}