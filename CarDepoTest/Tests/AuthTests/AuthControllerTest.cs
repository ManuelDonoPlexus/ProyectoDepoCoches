using CarDepo.API.Controllers;
using CarDepo.API.DTOs;
using CarDepo.API.Models;
using CarDepo.Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

namespace CarDepo.CarDepoTest.Tests.AuthTests;

public class AuthControllerTest
{
    private readonly Mock<AuthRepository> _authrepoMock;
    private readonly Mock<AuthService> _authserviceMock;
    private readonly AuthController _authcontroller;

    public AuthControllerTest()
    {
        _authrepoMock = new Mock<AuthRepository>();
        _authserviceMock = new Mock<AuthService>(_authrepoMock.Object);
        _authcontroller = new AuthController(_authserviceMock.Object);
    }

    [Fact]
    public async Task Register_Success()
    {
        UserDTO userDTO = new UserDTO
        {
            Name = "manolo",
            Password = "manolo",
            Email = "manolo@manolo" 
        };

        _authrepoMock.Setup(r => r.Register(userDTO));

        var result = _authcontroller.Register(userDTO);

        Assert.IsType<Ok>(result);
    }

    [Fact]
    public async Task Login_Success()
    {
        LoginDTO loginDTO = new LoginDTO
        {
            Password = "manolo",
            Email = "manolo@manolo" 
        };

        _authrepoMock.Setup(r => r.Login(loginDTO));

        var result = await _authcontroller.Login(loginDTO);

        Assert.IsType<String>(result);
    }
}