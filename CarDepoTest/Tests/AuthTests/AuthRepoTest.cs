using CarDepo.API.DTOs;
using CarDepo.Application.Utils;
using CarDepo.CarDepoTest.Tests.Utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

namespace CarDepo.CarDepoTest.Tests.AuthTests;

public class AuthRepoTest
{
    private readonly AuthRepository _authrepoMock;
    private readonly Mock<AuthUtilities> _authutils;

    public AuthRepoTest()
    {
        _authutils = new Mock<AuthUtilities>();
        _authrepoMock = new AuthRepository(new RepoClassTestUtil().ReturnFakeContext(), _authutils.Object);
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

        var result = _authrepoMock.Register(userDTO);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task Login_Success()
    {
        LoginDTO loginDTO = new LoginDTO
        {
            Password = "manolo",
            Email = "manolo@manolo" 
        };

        var result = _authrepoMock.Login(loginDTO);

        Assert.NotNull(result);
    }
}