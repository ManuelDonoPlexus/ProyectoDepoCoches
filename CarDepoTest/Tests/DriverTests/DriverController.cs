using CarDepo.API.Models;
using Moq;
using CarDepo.Infrastructure.Repositories;
using CarDepo.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using CarDepo.Application.Services;
using CarDepoTest.Tests.Utils;

namespace CarDepoTest.Tests.DriverTests;

public class DriverControllerTest
{
    private readonly Mock<DriverRepository> _driverrepoMock;
    private readonly Mock<DriverService> _driverserviceMock;
    private readonly DriverController _drivercontroller;

    public DriverControllerTest()
    {
        _driverrepoMock = new Mock<DriverRepository>(new RepoClassTestUtil().ReturnFakeContext());
        _driverserviceMock = new Mock<DriverService>(_driverrepoMock.Object);
        _drivercontroller = new DriverController(_driverserviceMock.Object);
    }

    [Fact]
    public async Task GetAllDrivers_ReturnsOk()
    {
        Driver testdriver = new Driver
        {
            Id = 1,
            Name = "Pepe",
            Dni = "11111111A",
            EmailAddr = "pepe@mail.com",
            PhoneNumber = 123456789,
            OwnerId = 1,
        };

        var drivers = new List<Driver>(){testdriver};

        var result = await _drivercontroller.GetAllDrivers();

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task CreateDriver_ReturnsBadRequestWhenNull()
    {        
        var result = await _drivercontroller.CreateDriver(null);

        Assert.IsType<BadRequestResult>(result?.Result);
    }

    [Fact]
    public async Task ModifyDriver_ReturnsOk()
    {
        Driver testdriver = new Driver
        {
            Name = "Pepe",
            Dni = "11111111A",
            EmailAddr = "pepe@mail.com",
            PhoneNumber = 123456789,
            OwnerId = 1,
        };

        var result = await _drivercontroller.ModifyDriver(1, testdriver);

        Assert.IsType<OkObjectResult>(result);
    }
}