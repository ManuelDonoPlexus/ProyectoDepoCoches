using CarDepo.API.Models;
using Moq;
using CarDepo.Infrastructure.Repositories;
using CarDepo.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using CarDepo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using CarDepo.Application.Services;

namespace CarDepo.CarDepoTest;

public class CarRepoTest
{

    [Fact]
    public async Task GetCar_ReturnsOkObjectResult()
    {
        var repoMock = new Mock<ICarRepository>();
        var controller = new CarController(repoMock.Object);
        var result = await controller.GetSpecificCar(1);
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task CreateCar_Fail()
    {
        var repoMock = new Mock<ICarRepository>();

        var controller = new CarController(repoMock.Object);
        var result = await controller.CreateCar(null);
        Assert.IsType<BadRequestResult>(result.Result);
    }

    [Fact]
    public async Task DeleteCar_NotFound()
    {
        var repoMock = new Mock<ICarRepository>();
        var controller = new CarController(repoMock.Object);
        var result = await controller.DeleteCar(-9);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task InsertContext_Success()
    {
        var context = new Mock<CarDepoContext>();
        var car = new Car
        {
            License = "1234-AB",
            Kms = 123,
            ColorId = 1,
            MakeId = 1,
            OwnerId = 1,
        };
    }

}