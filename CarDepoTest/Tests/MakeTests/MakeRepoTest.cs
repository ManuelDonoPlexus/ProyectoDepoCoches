using CarDepo.API.Models;
using CarDepo.Infrastructure.Repositories;
using CarDepoTest.Tests.Utils;

namespace CarDepoTest.Tests.MakeTests;

public class MakeRepoTest
{
    private readonly MakeRepository _makerepoMock;

    public MakeRepoTest(){
        _makerepoMock = new MakeRepository(new RepoClassTestUtil().ReturnFakeContext());
    }

    [Fact]
    public async Task GetMake_ReturnsMakes()
    {
        var result = await _makerepoMock.GetMakes();
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetMake_ReturnsDTOWhenExists()
    {
        var result = await _makerepoMock.GetMake(1);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetMake_ReturnsNullWhenNotExists()
    {
        var result = await _makerepoMock.GetMake(362562);
        Assert.Null(result);
    }

    [Fact]
    public async Task InsertMake_Success()
    {
        Make testmake = new Make
        {
            Id = 2,
            Name = "Seat",
            HorsePower = 100,
            Price = 1000.00M,
            FuelTypeId = 1
        };

        var result = await _makerepoMock.InsertMake(testmake);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateMake_Success()
    {
        Make testmake = new Make
        {
            Id = 1,
            Name = "Seat",
            HorsePower = 100,
            Price = 1000.00M,
            FuelTypeId = 1
        };

        var result = await _makerepoMock.UpdateMake(1,testmake);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateMake_ReturnsNullWhenNotFound()
    {
        Make testmake = new Make
        {
            Id = 9461232,
            Name = "Seat",
            HorsePower = 100,
            Price = 1000.00M,
            FuelTypeId = 1
        };

        var result = await _makerepoMock.UpdateMake(9461232,testmake);
        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteMake_Success()
    {
        await _makerepoMock.DeleteMake(1);
        var result = await _makerepoMock.GetMake(1);
        Assert.Null(result);
    }

    [Fact]
    public async Task IfMakeExists_Success()
    {
        var result = _makerepoMock.IfMakeExists(1);
        Assert.True(result);
    }

    [Fact]
    public async Task IfMakeDoesNotExists_Fail()
    {
        var result = _makerepoMock.IfMakeExists(935231311);
        Assert.False(result);
    }
}