using CarDepo.API.Models;
using Moq;
using CarDepo.Infrastructure.Repositories;
using CarDepo.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using CarDepo.Application.Services;
using CarDepo.CarDepoTest.Tests.Utils;

namespace CarDepo.CarDepoTest.Tests.OwnerTests;

public class OwnerControllerTest
{
    private readonly Mock<OwnerRepository> _ownerrepoMock;
    private readonly Mock<OwnerService> _ownerserviceMock;
    private readonly OwnerController _ownercontroller;

    public OwnerControllerTest()
    {
        _ownerrepoMock = new Mock<OwnerRepository>(new RepoClassTestUtil().ReturnFakeContext());
        _ownerserviceMock = new Mock<OwnerService>(_ownerrepoMock.Object);
        _ownercontroller = new OwnerController(_ownerserviceMock.Object);
    }

    [Fact]
    public async Task GetAllOwners_ReturnsOk()
    {
        var result = await _ownercontroller.GetAllOwners();

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task CreateOwner_ReturnsBadRequestWhenNull()
    {        
        var result = await _ownercontroller.CreateOwner(null);

        Assert.IsType<BadRequestResult>(result?.Result);
    }

    [Fact]
    public async Task ModifyOwner_ReturnsOk()
    {
        Owner testowner = new Owner
        {
            Id = 1,
            Name = "Test INC",
            Nif = "A11113333",
            PhoneNumber = 999222111,
            DateEntry = DateOnly.Parse("2001-01-01"),
            EmailAddr = "test@mail"
        };

        var result = await _ownercontroller.ModifyOwner(1, testowner);

        Assert.IsType<OkObjectResult>(result);
    }
}