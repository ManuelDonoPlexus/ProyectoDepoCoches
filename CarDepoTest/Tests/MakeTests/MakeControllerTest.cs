using CarDepo.API.Models;
using Moq;
using CarDepo.Infrastructure.Repositories;
using CarDepo.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using CarDepo.Application.Services;
using CarDepo.API.DTOs.Make;

namespace CarDepo.CarDepoTest.Tests.MakeTests;

public class CarControllerTest
{
    private readonly Mock<MakeRepository> _makerepoMock;
    private readonly Mock<MakeService> _makeserviceMock;
    private readonly MakeController _makecontroller;

    public CarControllerTest()
    {
        _makerepoMock = new Mock<MakeRepository>();
        _makeserviceMock = new Mock<MakeService>(_makerepoMock.Object);
        _makecontroller = new MakeController(_makeserviceMock.Object);
    }

    [Fact]
    public async Task GetAllCarDrivers_ReturnsOk()
    {
        Make testmake = new Make
        {
            Id = 1,
            Name = "Seat",
            HorsePower = 100,
            Price = 1000.00M,
            FuelTypeId = 1
        };

        var makes = new List<Make>(){testmake};

        _makerepoMock.Setup(r => r.GetMakes())
            .ReturnsAsync(makes);

        var result = await _makecontroller.GetAllMakes();

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task CreateCarDriver_ReturnsBadRequestWhenNull()
    {
        _makerepoMock.Setup(r => r.InsertMake(null))
            .Returns((Task<MakeDTO?>?)null);
        
        var result = await _makecontroller.CreateMake(null);

        Assert.IsType<BadRequestResult>(result?.Result);
    }

    [Fact]
    public async Task ModifyCarDriver_ReturnsOk()
    {
        Make testmake = new Make
        {
            Id = 1,
            Name = "Seat",
            HorsePower = 100,
            Price = 1000.00M,
            FuelTypeId = 1
        };

        var makeDTO = new MakeDTO
        {
            Id = testmake.Id,
            Name = testmake.Name,
            HorsePower = testmake.HorsePower,
            Price = 1500.00M,
            FuelTypeId = testmake.FuelTypeId
        };

        _makerepoMock.Setup(r => r.UpdateMake(1, testmake))
            .ReturnsAsync(makeDTO);

        var result = await _makecontroller.GetAllMakes();

        Assert.IsType<OkObjectResult>(result);
    }
}