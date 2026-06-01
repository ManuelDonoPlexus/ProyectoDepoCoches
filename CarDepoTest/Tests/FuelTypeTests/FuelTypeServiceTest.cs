using CarDepo.API.Models;
using Moq;
using CarDepo.Infrastructure.Repositories;
using CarDepo.Application.Services;
using CarDepo.API.DTOs.FuelType;

namespace CarDepo.CarDepoTest.Tests.FuelTypeTests;

public class FuelTypeServiceTest
{
    private readonly Mock<FuelTypeRepository> _fuelrepoMock;
    private readonly FuelTypeService _fuelserviceMock;

    public FuelTypeServiceTest()
    {
        _fuelrepoMock = new Mock<FuelTypeRepository>();
        _fuelserviceMock = new FuelTypeService(_fuelrepoMock.Object);
    }

    [Fact]
    public async Task InsertFuelType_ReturnsInsertedDriver()
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

        _fuelrepoMock.Setup(r => r.InsertFuelType(testfuel))
            .ReturnsAsync(fuelDTO);
        
        var result = await _fuelserviceMock.InsertFuelType(testfuel);

        Assert.NotNull(result);
        Assert.Equal(testfuel.Id, result.Id);
    }
}