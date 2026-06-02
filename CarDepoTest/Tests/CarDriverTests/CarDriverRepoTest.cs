using CarDepo.API.Models;
using CarDepo.Infrastructure.Repositories;
using CarDepo.CarDepoTest.Tests.Utils;

namespace CarDepoTest.Tests.CarDriverTests;

public class CarDriverRepoTest
{
    private readonly CarDriverRepository _cardriverrepoMock;

    public CarDriverRepoTest()
    {
        _cardriverrepoMock = new CarDriverRepository(new RepoClassTestUtil().ReturnFakeContext());
    }

    [Fact]
    public async Task GetCarDrivers_ReturnsCarDrivers()
    {
        var result = await _cardriverrepoMock.GetCarDrivers();

        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetCarDriver_ReturnsDTOWhenExists()
    {
        var result = await _cardriverrepoMock.GetCarDriver(1);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetCarDriver_ReturnsNullWhenNotExists()
    {
        var result = await _cardriverrepoMock.GetCarDriver(362562);

        Assert.Null(result);
    }

    [Fact]
    public async Task InsertCarDriver_Success()
    {
        CarDriver testcardriver = new CarDriver
        {
            Id = 2,
            DateDrive = DateOnly.Parse("2001-01-1"),
            CarCDId = 1,
            DriverCDId = 1
        };

        var result = await _cardriverrepoMock.InsertCarDriver(testcardriver);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateCarDriver_Success()
    {
        CarDriver testcardriver = new CarDriver
        {
            Id = 1,
            DateDrive = DateOnly.Parse("2007-06-5"),
            CarCDId = 1,
            DriverCDId = 1
        };

        var result = await _cardriverrepoMock.UpdateCarDriver(1, testcardriver);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateCarDriver_ReturnsNullWhenNotFound()
    {
        CarDriver testcardriver = new CarDriver
        {
            Id = 9461232,
            DateDrive = DateOnly.Parse("2007-06-5"),
            CarCDId = 1,
            DriverCDId = 1
        };

        var result = await _cardriverrepoMock.UpdateCarDriver(9461232, testcardriver);

        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteCarDriver_Success()
    {
        await _cardriverrepoMock.DeleteCarDriver(1);

        var result = await _cardriverrepoMock.GetCarDriver(1);

        Assert.Null(result);
    }

    [Fact]
    public async Task IfCarDriverExists_Success()
    {

        var result = _cardriverrepoMock.IfCarDriverExists(1);

        Assert.True(result);
    }

    [Fact]
    public async Task IfCarDriverDoesNotExists_Fail()
    {
        var result = _cardriverrepoMock.IfCarDriverExists(935231311);

        Assert.False(result);
    }
}