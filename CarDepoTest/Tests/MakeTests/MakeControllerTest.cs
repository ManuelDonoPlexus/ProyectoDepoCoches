using CarDepo.API.Models;
using Moq;
using CarDepo.Infrastructure.Repositories;
using CarDepo.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using CarDepo.Application.Services;
using CarDepo.API.DTOs.Make;
using CarDepo.CarDepoTest.Tests.Utils;

namespace CarDepo.CarDepoTest.Tests.MakeTests;

public class CarControllerTest
{
    private readonly Mock<MakeRepository> _makerepoMock;
    private readonly Mock<MakeService> _makeserviceMock;
    private readonly MakeController _makecontroller;

    public CarControllerTest()
    {
        _makerepoMock = new Mock<MakeRepository>(new RepoClassTestUtil().ReturnFakeContext());
        _makeserviceMock = new Mock<MakeService>(_makerepoMock.Object);
        _makecontroller = new MakeController(_makeserviceMock.Object);
    }

    [Fact]
    public async Task GetAllMakes_ReturnsOk()
    {
        var result = await _makecontroller.GetAllMakes();

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task CreateMake_ReturnsBadRequestWhenNull()
    {
        var result = await _makecontroller.CreateMake(null);

        Assert.IsType<BadRequestResult>(result?.Result);
    }

    [Fact]
    public async Task ModifyMake_ReturnsOk()
    {
        Make testmake = new Make
        {
            Name = "Seat",
            HorsePower = 100,
            Price = 1000.00M,
            FuelTypeId = 1
        };

        var result = await _makecontroller.ModifyMake(1, testmake);

        Assert.IsType<OkObjectResult>(result);
    }
}