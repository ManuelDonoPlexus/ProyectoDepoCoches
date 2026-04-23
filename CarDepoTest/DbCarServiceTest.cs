using CarDepo.API.Models;
using Moq;
using CarDepo.Infrastructure.Repositories;
using CarDepo.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using CarDepo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CarDepo.CarDepoTest;

public class CarRepoTest
{
    private readonly static Mock<ICarRepository> carRepo = new Mock<ICarRepository>();
    private readonly CarController carController = new CarController((ICarRepository)carRepo);

    [Fact]
    public async Task TestPostSuccesful()
    {
        Car car = new Car()
        {
            License = "8156-GH",
            Kms = 123,
            ColorId = 1,
            OwnerId = 1,
            MakeId = 1
        };

        var result = await carController.CreateCar(car);

        Assert.IsType<ActionResult<Car>>(result);
    }

    [Fact]
    public async Task TestPostFailure()
    {
        Car car = new Car()
        {
            License = "8156-GH",
            Kms = 123,
            ColorId = 1,
            OwnerId = 1,
            MakeId = -9
        };

        await carController.CreateCar(car);
    }

    [Fact]
    public async Task TestPutSuccesful()
    {
        int carId = 1;
        Car car = new Car()
        {
            Id = 1,
            License = "8156-GH",
            Kms = 123,
            ColorId = 1,
            OwnerId = 1,
            MakeId = 1
        };

        await carController.ModifyCar(carId, car);
    }

    [Fact]
    public async Task TestPutFailure()
    {
        int carId = 0;
        Car car = new Car()
        {
            Id = 1,
            License = "8156-GH",
            Kms = 123,
            ColorId = 1,
            OwnerId = 1,
            MakeId = 1
        };

        await carController.ModifyCar(carId, car);
    }

    [Fact]
    public async Task TestDeleteSuccesful()
    {
        int carId = 0;

        await carController.DeleteCar(carId);
    }

    [Fact]
    public async Task TestDeleteFailure()
    {
        int carId = -9;

        await carController.DeleteCar(carId);
    }

    [Fact]
    public async Task TestGetSuccesful()
    {
        int carId = 9;

        await carController.GetSpecificCar(carId);
    }

    [Fact]
    public async Task TestGetFailure()
    {
        int carId = 9;

        await carController.GetSpecificCar(carId);
    }

}