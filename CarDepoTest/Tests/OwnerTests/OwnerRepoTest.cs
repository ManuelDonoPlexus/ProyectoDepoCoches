using CarDepo.API.Models;
using CarDepo.Infrastructure.Repositories;
using CarDepo.CarDepoTest.Tests.Utils;

namespace CarDepo.CarDepoTest.Tests.OwnerTests;

public class OwnerRepoTest
{
    private readonly OwnerRepository _ownerrepoMock;

    public OwnerRepoTest(){
        _ownerrepoMock = new OwnerRepository(new RepoClassTestUtil().ReturnFakeContext());
    }

    [Fact]
    public async Task GetMake_ReturnsDrivers()
    {
        var result = await _ownerrepoMock.GetOwners();
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetMake_ReturnsDTOWhenExists()
    {
        var result = await _ownerrepoMock.GetOwner(1);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetMake_ReturnsNullWhenNotExists()
    {
        var result = await _ownerrepoMock.GetOwner(362562);
        Assert.Null(result);
    }

    [Fact]
    public async Task InsertMake_Success()
    {
        Owner testowner = new Owner
        {
            Id = 2,
            Name = "Test INC",
            Nif = "A11113333",
            PhoneNumber = 999222111,
            DateEntry = DateOnly.Parse("2001-01-01"),
            EmailAddr = "test@mail"
        };

        var result = await _ownerrepoMock.InsertOwner(testowner);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateFuelType_Success()
    {
        Owner testowner = new Owner
        {
            Id = 2,
            Name = "Test INC",
            Nif = "A11113333",
            PhoneNumber = 999222111,
            DateEntry = DateOnly.Parse("2001-01-01"),
            EmailAddr = "test@mail"
        };

        var result = await _ownerrepoMock.UpdateOwner(1,testowner);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateFuelType_ReturnsNullWhenNotFound()
    {
        Owner testowner = new Owner
        {
            Id = 2,
            Name = "Test INC",
            Nif = "A11113333",
            PhoneNumber = 999222111,
            DateEntry = DateOnly.Parse("2001-01-01"),
            EmailAddr = "test@mail"
        };

        var result = await _ownerrepoMock.UpdateOwner(9461232,testowner);
        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteFuelType_Success()
    {
        await _ownerrepoMock.DeleteOwner(1);
        var result = await _ownerrepoMock.GetOwner(1);
        Assert.Null(result);
    }

    [Fact]
    public async Task IfFuelTypeExists_Success()
    {
        var result = _ownerrepoMock.IfOwnerExists(1);
        Assert.True(result);
    }

    [Fact]
    public async Task IfFuelTypeDoesNotExists_Fail()
    {
        var result = _ownerrepoMock.IfOwnerExists(935231311);
        Assert.False(result);
    }
}