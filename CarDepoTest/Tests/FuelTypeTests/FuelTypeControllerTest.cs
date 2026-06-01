using CarDepo.API.Models;
using Moq;
using CarDepo.Infrastructure.Repositories;
using CarDepo.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using CarDepo.Application.Services;
using CarDepo.API.DTOs.FuelType;

namespace CarDepo.CarDepoTest.Tests.FuelTypeTests;

public class FuelTypeControllerTest
{
    private readonly Mock<FuelTypeRepository> _fuelrepoMock;
    private readonly Mock<FuelTypeService> _fuelserviceMock;
    private readonly FuelTypeController _fuelcontroller;

    public FuelTypeControllerTest()
    {
        _fuelrepoMock = new Mock<FuelTypeRepository>();
        _fuelserviceMock = new Mock<FuelTypeService>(_fuelrepoMock.Object);
        _fuelcontroller = new FuelTypeController(_fuelserviceMock.Object);
    }

    [Fact]
    public async Task GetAllFuelTypes_ReturnsOk()
    {
        FuelType testfuel = new FuelType
        {
            Id = 1,
            Name = "Diesel"
        };

        var fines = new List<FuelType>(){testfuel};

        _fuelrepoMock.Setup(r => r.GetFuelTypes())
            .ReturnsAsync(fines);

        var result = await _fuelcontroller.GetAllFuelTypes();

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task CreateFuelType_ReturnsBadRequestWhenNull()
    {
        _fuelrepoMock.Setup(r => r.InsertFuelType(null))
            .Returns((Task<FuelTypeDTO?>)null);
        
        var result = await _fuelcontroller.CreateFuelType(null);

        Assert.IsType<BadRequestResult>(result?.Result);
    }

    [Fact]
    public async Task ModifyFuelType_ReturnsOk()
    {
        FuelType testfuel = new FuelType
        {
            Id = 1,
            Name = "Diesel"
        };

        FuelTypeDTO fuelDTO = new FuelTypeDTO
        {
            Id = testfuel.Id,
            Name = "Gasolina"
        };

        _fuelrepoMock.Setup(r => r.UpdateFuelType(1, testfuel))
            .ReturnsAsync(fuelDTO);

        var result = await _fuelcontroller.GetAllFuelTypes();

        Assert.IsType<OkObjectResult>(result);
    }
}