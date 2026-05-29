using CarDepo.API.Models;
using Moq;
using CarDepo.Infrastructure.Repositories;
using CarDepo.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using CarDepo.Application.Services;
using CarDepo.API.DTOs.Car;

namespace CarDepo.CarDepoTest.CarTests;

public class CarServiceTest
{
    private readonly Mock<CarRepository> _carrepoMock;
    private readonly CarService _carserviceMock;

    public CarServiceTest()
    {
        _carrepoMock = new Mock<CarRepository>();
        _carserviceMock = new CarService(_carrepoMock.Object);
    }

    [Fact]
    public async Task InsertCar_ReturnsInsertedCar()
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

        var cardto = new CarDTO
        {
            Id = testcar.Id,
            License = testcar.License,
            Kms = testcar.Kms,
            ColorId = testcar.ColorId,
            MakeId = testcar.ColorId,
            OwnerId = testcar.OwnerId
        };

        _carrepoMock.Setup(r => r.InsertCar(testcar))
            .ReturnsAsync(cardto);
        
        var result = await _carserviceMock.InsertCar(testcar);

        Assert.NotNull(result);
        Assert.Equal(testcar.License, result.License);
    }
}