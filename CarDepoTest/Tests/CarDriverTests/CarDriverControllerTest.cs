using CarDepo.API.Models;
using Moq;
using CarDepo.Infrastructure.Repositories;
using CarDepo.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using CarDepo.Application.Services;
using CarDepo.API.DTOs.CarDriver;

namespace CarDepo.CarDepoTest.Tests.CarDriverTests;

public class CarControllerTest
{
    private readonly Mock<CarDriverRepository> _cardriverrepoMock;
    private readonly Mock<CarDriverService> _cardriverserviceMock;
    private readonly CarDriverController _cardrivercontroller;

    public CarControllerTest()
    {
        _cardriverrepoMock = new Mock<CarDriverRepository>();
        _cardriverserviceMock = new Mock<CarDriverService>(_cardriverrepoMock.Object);
        _cardrivercontroller = new CarDriverController(_cardriverserviceMock.Object);
    }

    [Fact]
    public async Task GetAllCarDrivers_ReturnsOk()
    {
        CarDriver testcardriver = new CarDriver
        {
            DateDrive = DateOnly.Parse("2001-01-1"),
            CarCDId = 1,
            DriverCDId = 1
        };

        var cardrivers = new List<CarDriver>(){testcardriver};

        _cardriverrepoMock.Setup(r => r.GetCarDrivers())
            .ReturnsAsync(cardrivers);

        var result = await _cardrivercontroller.GetAllCarDrivers();

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task CreateCarDriver_ReturnsBadRequestWhenNull()
    {
        _cardriverrepoMock.Setup(r => r.InsertCarDriver(null))
            .Returns((Task<CarDriverDTO?>?)null);
        
        var result = await _cardrivercontroller.CreateCarDriver(null);

        Assert.IsType<BadRequestResult>(result?.Result);
    }

    [Fact]
    public async Task ModifyCarDriver_ReturnsOk()
    {
        CarDriver testcardriver = new CarDriver
        {
            Id = 1,
            DateDrive = DateOnly.Parse("2001-01-1"),
            CarCDId = 1,
            DriverCDId = 1
        };

        var cardriverDTO = new CarDriverDTO
        {
            Id = testcardriver.Id,
            DateDrive = DateOnly.Parse("2004-05-19"),
            CarCDId = testcardriver.CarCDId,
            DriverCDId = testcardriver.DriverCDId
        };

        _cardriverrepoMock.Setup(r => r.UpdateCarDriver(1, testcardriver))
            .ReturnsAsync(cardriverDTO);

        var result = await _cardrivercontroller.GetAllCarDrivers();

        Assert.IsType<OkObjectResult>(result);
    }
}