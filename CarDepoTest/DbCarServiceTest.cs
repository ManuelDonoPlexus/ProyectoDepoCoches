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
    // UNIT TEST

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
        Assert.IsType<BadRequestResult>(result!.Result);
    }

    [Fact]
    public async Task DeleteCar_NotContentResult()
    {
        var repoMock = new Mock<ICarRepository>();
        var controller = new CarController(repoMock.Object);
        var result = await controller.DeleteCar(-9);
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DBContext()
    {
        var mockSet = new Mock<DbSet<Car>>();
        var mockContext = new Mock<CarDepoContext>();
        mockContext.Setup(c => c.Cars).Returns(mockSet.Object);

        var service = new CarServiceDbContext(mockContext.Object);

        var License = "ABCD-123";
        var Kms = 123;
        var ColorId = 1;
        var OwnerId = 1;
        var MakeId = 1;

        var result = await service.insertCarWithValidation(License,Kms,ColorId,OwnerId,MakeId);
        Assert.True(result);
    }
}