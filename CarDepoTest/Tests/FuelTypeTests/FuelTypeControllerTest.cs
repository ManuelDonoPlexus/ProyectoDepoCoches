using CarDepo.API.Models;
using Moq;
using CarDepo.Infrastructure.Repositories;
using CarDepo.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using CarDepo.Application.Services;
using CarDepo.API.DTOs.FuelType;
using CarDepo.CarDepoTest.Tests.Utils;

namespace CarDepo.CarDepoTest.Tests.FuelTypeTests;

public class FuelTypeControllerTest
{
    private readonly Mock<FuelTypeRepository> _fuelrepoMock;
    private readonly Mock<FuelTypeService> _fuelserviceMock;
    private readonly FuelTypeController _fuelcontroller;

    public FuelTypeControllerTest()
    {
        _fuelrepoMock = new Mock<FuelTypeRepository>(new RepoClassTestUtil().ReturnFakeContext());
        _fuelserviceMock = new Mock<FuelTypeService>(_fuelrepoMock.Object);
        _fuelcontroller = new FuelTypeController(_fuelserviceMock.Object);
    }

    [Fact]
    public async Task GetAllFuelTypes_ReturnsOk()
    {
        var result = await _fuelcontroller.GetAllFuelTypes();

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task CreateFuelType_ReturnsBadRequestWhenNull()
    {        
        var result = await _fuelcontroller.CreateFuelType(null);

        Assert.IsType<BadRequestResult>(result?.Result);
    }

    [Fact]
    public async Task ModifyFuelType_ReturnsOk()
    {
        FuelType testfuel = new FuelType
        {
            Name = "Electrico"
        };

        var result = await _fuelcontroller.ModifyFuelType(1,testfuel);

        Assert.IsType<OkObjectResult>(result);
    }
}