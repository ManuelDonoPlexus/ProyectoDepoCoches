using CarDepo.API.Models;
using Moq;
using CarDepo.Infrastructure.Repositories;
using CarDepo.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using CarDepo.Application.Services;
using CarDepo.CarDepoTest.Tests.Utils;

namespace CarDepo.CarDepoTest.Tests.CarTests;

public class CarControllerTest
{
    private readonly Mock<CarRepository> _carrepoMock;
    private readonly Mock<CarService> _carserviceMock;
    private readonly CarController _carcontroller;

    public CarControllerTest()
    {
        _carrepoMock = new Mock<CarRepository>(new RepoClassTestUtil().ReturnFakeContext());
        _carserviceMock = new Mock<CarService>(_carrepoMock.Object);
        _carcontroller = new CarController(_carserviceMock.Object);
    }

    [Fact]
    public async Task GetAllCars_ReturnsOk()
    {
        var result = await _carcontroller.GetAllCars();

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task CreateCar_ReturnsBadRequestWhenNull()
    {
        var result = await _carcontroller.CreateCar(null);

        Assert.IsType<BadRequestResult>(result?.Result);
    }

    [Fact]
    public async Task ModifyCar_ReturnsOk()
    {
        Car testcar = new Car
        {
            License = "1234-ABC",
            Kms = 100,
            ColorId = 1,
            MakeId = 1,
            OwnerId = 1
        };

        var result = await _carcontroller.ModifyCar(1, testcar);

        Assert.IsType<OkObjectResult>(result);
    }
}