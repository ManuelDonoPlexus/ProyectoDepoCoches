using CarDepo.API.Models;
using Moq;
using CarDepo.Infrastructure.Repositories;
using CarDepo.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using CarDepo.Application.Services;
using CarDepo.API.DTOs.Car;

namespace CarDepo.CarDepoTest.Tests.CarTests;

public class CarControllerTest
{
    private readonly Mock<CarRepository> _carrepoMock;
    private readonly Mock<CarService> _carserviceMock;
    private readonly CarController _carcontroller;

    public CarControllerTest()
    {
        _carrepoMock = new Mock<CarRepository>();
        _carserviceMock = new Mock<CarService>(_carrepoMock.Object);
        _carcontroller = new CarController(_carserviceMock.Object);
    }

    [Fact]
    public async Task GetAllCars_ReturnsOk()
    {
        Car testcar = new Car
        {
            Id = 1,
            License = "1234-ABC",
            Kms = 100,
            ColorId = 1,
            MakeId = 1,
            OwnerId = 1
        };

        var cars = new List<Car>(){testcar};

        _carrepoMock.Setup(r => r.GetCars())
            .ReturnsAsync(cars);

        var result = await _carcontroller.GetAllCars();

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task CreateCar_ReturnsBadRequestWhenNull()
    {
        _carrepoMock.Setup(r => r.InsertCar(null))
            .Returns((Task<CarDTO?>)null);
        
        var result = await _carcontroller.CreateCar(null);

        Assert.IsType<BadRequestResult>(result?.Result);
    }

    [Fact]
    public async Task ModifyCar_ReturnsOk()
    {
        Car testcar = new Car
        {
            Id = 1,
            License = "1234-ABC",
            Kms = 100,
            ColorId = 1,
            MakeId = 1,
            OwnerId = 1
        };

        var carDTO = new CarDTO
        {
            Id = testcar.Id,
            License = testcar.License,
            Kms = 300,
            ColorId = 2,
            MakeId = testcar.ColorId,
            OwnerId = testcar.OwnerId
        };

        _carrepoMock.Setup(r => r.UpdateCar(1, testcar))
            .ReturnsAsync(carDTO);

        var result = await _carcontroller.GetAllCars();

        Assert.IsType<OkObjectResult>(result);
    }
}