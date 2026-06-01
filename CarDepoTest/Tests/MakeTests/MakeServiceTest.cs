using CarDepo.API.Models;
using Moq;
using CarDepo.Infrastructure.Repositories;
using CarDepo.Application.Services;
using CarDepo.API.DTOs.Make;

namespace CarDepo.CarDepoTest.Tests.MakeTests;

public class FuelTypeServiceTest
{
    private readonly Mock<MakeRepository> _makerepoMock;
    private readonly MakeService _makeserviceMock;

    public FuelTypeServiceTest()
    {
        _makerepoMock = new Mock<MakeRepository>();
        _makeserviceMock = new MakeService(_makerepoMock.Object);
    }

    [Fact]
    public async Task InsertFuelType_ReturnsInsertedDriver()
    {
        Make testmake = new Make
        {
            Id = 1,
            Name = "Seat",
            HorsePower = 100,
            Price = 1000.00M,
            FuelTypeId = 1
        };

        var makeDTO = new MakeDTO
        {
            Id = testmake.Id,
            Name = testmake.Name,
            HorsePower = testmake.HorsePower,
            Price = 1500.00M,
            FuelTypeId = testmake.FuelTypeId
        };

        _makerepoMock.Setup(r => r.InsertMake(testmake))
            .ReturnsAsync(makeDTO);
        
        var result = await _makeserviceMock.InsertMake(testmake);

        Assert.NotNull(result);
        Assert.Equal(testmake.Id, result.Id);
    }
}