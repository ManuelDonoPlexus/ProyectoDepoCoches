using CarDepo.API.Models;
using Moq;
using CarDepo.Infrastructure.Repositories;
using CarDepo.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using CarDepo.Application.Services;
using CarDepoTest.Tests.Utils;

namespace CarDepoTest.Tests.CarDriverTests;

public class CarDriverControllerTest
{
    private readonly Mock<CarDriverRepository> _cardriverrepoMock;
    private readonly Mock<CarDriverService> _cardriverserviceMock;
    private readonly CarDriverController _cardrivercontroller;

    public CarDriverControllerTest()
    {
        _cardriverrepoMock = new Mock<CarDriverRepository>(new RepoClassTestUtil().ReturnFakeContext());
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

        var result = await _cardrivercontroller.GetAllCarDrivers();

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task CreateCarDriver_ReturnsBadRequestWhenNull()
    {
        var result = await _cardrivercontroller.CreateCarDriver(null);

        Assert.IsType<BadRequestResult>(result?.Result);
    }

    [Fact]
    public async Task ModifyCarDriver_ReturnsOk()
    {
        CarDriver testcardriver = new CarDriver
        {
            DateDrive = DateOnly.Parse("2001-01-1"),
            CarCDId = 1,
            DriverCDId = 1
        };

        var result = await _cardrivercontroller.ModifyCarDriver(1, testcardriver);

        Assert.IsType<OkObjectResult>(result);
    }
}