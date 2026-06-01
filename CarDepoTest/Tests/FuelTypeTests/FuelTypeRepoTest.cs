using CarDepo.API.Models;
using CarDepo.Infrastructure.Repositories;
using CarDepo.CarDepoTest.Tests.Utils;

namespace CarDepo.CarDepoTest.Tests.FuelTypeTests;

public class FuelTypeRepoTest
{
    private readonly FuelTypeRepository _fuelrepoMock;

    public FuelTypeRepoTest(){
        _fuelrepoMock = new FuelTypeRepository(new RepoClassTestUtil().ReturnFakeContext());
    }

    [Fact]
    public async Task GetFuelType_ReturnsNotNull()
    {
        var result = await _fuelrepoMock.GetFuelTypes();
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetFuelType_ReturnsDTOWhenExists()
    {
        var result = await _fuelrepoMock.GetFuelType(1);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetFuelType_ReturnsNullWhenNotExists()
    {
        var result = await _fuelrepoMock.GetFuelType(362562);
        Assert.Null(result);
    }

    [Fact]
    public async Task InsertFuelType_Success()
    {
        FuelType testfuel = new FuelType
        {
            Id = 2,
            Name = "Gasolina"
        };

        var result = await _fuelrepoMock.InsertFuelType(testfuel);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateFuelType_Success()
    {
        FuelType testfuel = new FuelType
        {
            Id = 1,
            Name = "Gasolina"
        };

        var result = await _fuelrepoMock.UpdateFuelType(1,testfuel);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateFuelType_ReturnsNullWhenNotFound()
    {
        FuelType testfuel = new FuelType
        {
            Id = 2,
            Name = "Gasolina"
        };

        var result = await _fuelrepoMock.UpdateFuelType(9461232,testfuel);
        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteFuelType_Success()
    {
        await _fuelrepoMock.DeleteFuelType(1);
        var result = await _fuelrepoMock.GetFuelType(1);
        Assert.Null(result);
    }

    [Fact]
    public async Task IfFuelTypeExists_Success()
    {
        var result = _fuelrepoMock.IfFuelTypeExists(1);
        Assert.True(result);
    }

    [Fact]
    public async Task IfFuelTypeDoesNotExists_Fail()
    {
        var result = _fuelrepoMock.IfFuelTypeExists(935231311);
        Assert.False(result);
    }
}