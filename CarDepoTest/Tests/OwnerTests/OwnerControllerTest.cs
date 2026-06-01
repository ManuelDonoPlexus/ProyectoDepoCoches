using CarDepo.API.Models;
using Moq;
using CarDepo.Infrastructure.Repositories;
using CarDepo.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using CarDepo.Application.Services;
using CarDepo.API.DTOs.Make;
using CarDepo.API.DTOs.Owner;

namespace CarDepo.CarDepoTest.Tests.MakeTests;

public class OwnerControllerTest
{
    private readonly Mock<OwnerRepository> _ownerrepoMock;
    private readonly Mock<OwnerService> _ownerserviceMock;
    private readonly OwnerController _ownercontroller;

    public OwnerControllerTest()
    {
        _ownerrepoMock = new Mock<OwnerRepository>();
        _ownerserviceMock = new Mock<OwnerService>(_ownerrepoMock.Object);
        _ownercontroller = new OwnerController(_ownerserviceMock.Object);
    }

    [Fact]
    public async Task GetAllCarDrivers_ReturnsOk()
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

        var makes = new List<Owner>(){testowner};

        _ownerrepoMock.Setup(r => r.GetOwners())
            .ReturnsAsync(makes);

        var result = await _ownercontroller.GetAllOwners();

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task CreateCarDriver_ReturnsBadRequestWhenNull()
    {
        _ownerrepoMock.Setup(r => r.InsertOwner(null))
            .Returns((Task<OwnerDTO?>?)null);
        
        var result = await _ownercontroller.CreateOwner(null);

        Assert.IsType<BadRequestResult>(result?.Result);
    }

    [Fact]
    public async Task ModifyCarDriver_ReturnsOk()
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

        var ownerDTO = new OwnerDTO
        {
            Id = testowner.Id,
            Name = testowner.Name,
            Nif = testowner.Nif,
            PhoneNumber = 923145678,
            DateEntry = testowner.DateEntry,
            EmailAddr = testowner.EmailAddr
        };

        _ownerrepoMock.Setup(r => r.UpdateOwner(1, testowner))
            .ReturnsAsync(ownerDTO);

        var result = await _ownercontroller.GetAllOwners();

        Assert.IsType<OkObjectResult>(result);
    }
}