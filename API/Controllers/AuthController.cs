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


namespace CarDepo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly CarDepoContext _cardepocontext;
        public readonly AuthUtilities _authutils;

        public AuthController(CarDepoContext context, AuthUtilities utilities)
        {
            _cardepocontext = context;
            _authutils = utilities;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserDTO user)
        {
            var newUser = new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = _authutils.EncryptToSHA256(user.Password)
            };

            await _cardepocontext.Users.AddAsync(newUser);
            await _cardepocontext.SaveChangesAsync();
            return Ok(newUser);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            var foundUser = await _cardepocontext.Users
                                    .Where(u =>
                                            u.Email == login.Email &&
                                            u.Password == _authutils.EncryptToSHA256(login.Password) || u.Password == login.Password)
                                    .FirstOrDefaultAsync();


            if (foundUser != null) { return Ok(_authutils.GenerateJWTToken(foundUser)); }
            else { return NotFound(); }
        }
    }
}