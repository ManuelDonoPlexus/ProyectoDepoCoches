using CarDepo.API.Models;
using CarDepo.Infrastructure.Repositories;
using CarDepo.CarDepoTest.Tests.Utils;

namespace CarDepo.CarDepoTest.Tests.FineTests;

public class FineRepoTest
{
    private readonly FineRepository _finerepoMock;

    public FineRepoTest(){
        _finerepoMock = new FineRepository(new RepoClassTestUtil().ReturnFakeContext());
    }

    [Fact]
    public async Task GetFines_ReturnsDrivers()
    {
        var result = await _finerepoMock.GetFines();

        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetFine_ReturnsDTOWhenExists()
    {
        var result = await _finerepoMock.GetFine(1);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetFine_ReturnsNullWhenNotExists()
    {
        var result = await _finerepoMock.GetFine(362562);

        Assert.Null(result);
    }

    [Fact]
    public async Task InsertFine_Success()
    {
        Fine testfine = new Fine
        {
            Id = 1,
            Price = 100.00M,
            Payed = true,
            Description = "",
            Date = DateOnly.Parse("2001-01-1"),
            OwnerId = 1,
            CarId = 1
        };

        var result = await _finerepoMock.InsertFine(testfine);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateFine_Success()
    {
        Fine testfine = new Fine
        {
            Id = 1,
            Price = 100.00M,
            Payed = true,
            Description = "",
            Date = DateOnly.Parse("2001-01-1"),
            OwnerId = 1,
            CarId = 1
        };

        var result = await _finerepoMock.UpdateFine(1,testfine);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateFine_ReturnsNullWhenNotFound()
    {
        Fine testfine = new Fine
        {
            Id = 9461232,
            Price = 100.00M,
            Payed = true,
            Description = "",
            Date = DateOnly.Parse("2001-01-1"),
            OwnerId = 1,
            CarId = 1
        };

        var result = await _finerepoMock.UpdateFine(9461232,testfine);

        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteFine_Success()
    {
        await _finerepoMock.DeleteFine(1);

        var result = await _finerepoMock.GetFine(1);

        Assert.Null(result);
    }

    [Fact]
    public async Task IfFineExists_Success()
    {

        var result = _finerepoMock.IfFineExists(1);

        Assert.True(result);
    }

    [Fact]
    public async Task IfFineDoesNotExists_Fail()
    {
        var result = _finerepoMock.IfFineExists(935231311);

        Assert.False(result);
    }
}