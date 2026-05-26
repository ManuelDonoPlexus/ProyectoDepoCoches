using Microsoft.AspNetCore.Mvc;
using CarDepo.API.DTOs;
using Microsoft.AspNetCore.Authorization;
using CarDepo.Application.Services;


namespace CarDepo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authservice;

        public AuthController(AuthService authService)
        {
            _authservice = authService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserDTO user)
        {
            return Ok(_authservice.Register(user));
        }

        [HttpPost]
        [Route("login")]
        public async Task<String?> Login(LoginDTO login)
        {
            String? token = await _authservice.Login(login);
            if (token != null) { return token; }
            else { return null; }
             
        }
    }
}