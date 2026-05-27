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
    public async Task GetSpecificCar_ReturnsOkObjectResult()
    {
        var context = new Mock<CarDepoContext>();
        var repoMock = new Mock<CarRepository>(context.Object);
        var serviceMock = new Mock<CarService>(repoMock.Object);
        var controller = new CarController(serviceMock.Object);
        var result = await controller.GetSpecificCar(1);
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task CreateCar_Fail()
    {
        var repoMock = new Mock<CarRepository>();
        var serviceMock = new Mock<CarService>(repoMock.Object);
        var controller = new CarController(serviceMock.Object);
        var result = await controller.CreateCar(null);
        Assert.IsType<BadRequestResult>(result!.Result);
    }

    [Fact]
    public async Task DeleteCar_NotContentResult()
    {
        var repoMock = new Mock<CarRepository>();
        var serviceMock = new Mock<CarService>(repoMock.Object);
        var controller = new CarController(serviceMock.Object);
        var result = await controller.DeleteCar(-9);
        Assert.IsType<NoContentResult>(result);
    }
}