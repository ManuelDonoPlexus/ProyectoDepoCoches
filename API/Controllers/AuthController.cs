using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;
using CarDepo.Infrastructure.Repositories;
using CarDepo.API.DTOs;
using CarDepo.API.Utils;
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