using CarDepo.API.DTOs;
using CarDepo.API.Models;
using CarDepo.API.Utils;
using CarDepo.Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public interface IAuthRepository
{
    public Task Register(UserDTO user);
    public Task<String?> Login(LoginDTO login);
}

public class AuthRepository : IAuthRepository
{
    private readonly CarDepoContext _cardepocontext;
    private readonly AuthUtilities _authutils;

    public AuthRepository(CarDepoContext context, AuthUtilities utilities)
    {
        _cardepocontext = context;
        _authutils = utilities;
    }

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