using CarDepo.API.Models;
using Moq;
using CarDepo.Infrastructure.Repositories;
using CarDepo.Application.Services;
using CarDepo.CarDepoTest.Tests.Utils;

namespace CarDepoTest.Tests.FuelTypeTests;

public class FuelTypeServiceTest
{
    private readonly Mock<FuelTypeRepository> _fuelrepoMock;
    private readonly FuelTypeService _fuelserviceMock;

    public FuelTypeServiceTest()
    {
        _fuelrepoMock = new Mock<FuelTypeRepository>(new RepoClassTestUtil().ReturnFakeContext());
        _fuelserviceMock = new FuelTypeService(_fuelrepoMock.Object);
    }

    [Fact]
    public async Task InsertFuelType_ReturnsInsertedFuelType()
    {
        FuelType testfuel = new FuelType
        {
            Name = "Diesel"
        };
        
        var result = await _fuelserviceMock.InsertFuelType(testfuel);

        Assert.NotNull(result);
        Assert.Equal(testfuel.Id, result.Id);
    }
}