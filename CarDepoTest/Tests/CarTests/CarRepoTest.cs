using CarDepo.API.Models;
using Moq;
using CarDepo.Infrastructure.Repositories;
using CarDepoTest.Tests.Utils;

namespace CarDepoTest.Tests.CarTests;

public class CarRepoTest
{
    private readonly CarRepository _carrepoMock;

    public CarRepoTest(){
        _carrepoMock = new CarRepository(new RepoClassTestUtil().ReturnFakeContext());
    }

    [Fact]
    public async Task GetCars_ReturnsCars()
    {
        var result = await _carrepoMock.GetCars();

        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetCar_ReturnsDTOWhenExists()
    {
        var result = await _carrepoMock.GetCar(1);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetCar_ReturnsNullWhenNotExists()
    {
        var result = await _carrepoMock.GetCar(362562);

        Assert.Null(result);
    }

    [Fact]
    public async Task InsertCar_Success()
    {
        var newcartest = new Car
        {
            Id = 2,
            License = "5678-DEF",
            Kms = 200,
            ColorId = 1,
            MakeId = 1,
            OwnerId = 1
        };

        var result = await _carrepoMock.InsertCar(newcartest);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateCar_Success()
    {
        var newcartest = new Car
        {
            Id = 1,
            License = "9101112-GHI",
            Kms = 300,
            ColorId = 1,
            MakeId = 1,
            OwnerId = 1
        };

        var result = await _carrepoMock.UpdateCar(1,newcartest);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateCar_ReturnsNullWhenNotFound()
    {
        var newcartest = new Car
        {
            Id = 9461232,
            License = "9101112-GHI",
            Kms = 300,
            ColorId = 1,
            MakeId = 1,
            OwnerId = 1
        };

        var result = await _carrepoMock.UpdateCar(9461232,newcartest);

        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteCar_Success()
    {
        await _carrepoMock.DeleteCar(1);

        var result = await _carrepoMock.GetCar(1);

        Assert.Null(result);
    }

    [Fact]
    public async Task IfCarExists_Success()
    {

        var result = _carrepoMock.IfCarExists(1);

        Assert.True(result);
    }

    [Fact]
    public async Task IfCarDoesNotExists_Fail()
    {
        var result = _carrepoMock.IfCarExists(935231311);

        Assert.False(result);
    }
}