using CarDepo.API.Models;
using CarDepo.Infrastructure.Repositories;
using CarDepoTest.Tests.Utils;

namespace CarDepoTest.Tests.OwnerTests;

public class OwnerRepoTest
{
    private readonly OwnerRepository _ownerrepoMock;

    public OwnerRepoTest(){
        _ownerrepoMock = new OwnerRepository(new RepoClassTestUtil().ReturnFakeContext());
    }

    [Fact]
    public async Task GetOwner_ReturnsOwners()
    {
        var result = await _ownerrepoMock.GetOwners();
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetOwner_ReturnsDTOWhenExists()
    {
        var result = await _ownerrepoMock.GetOwner(1);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetOwner_ReturnsNullWhenNotExists()
    {
        var result = await _ownerrepoMock.GetOwner(362562);
        Assert.Null(result);
    }

    [Fact]
    public async Task InsertOwner_Success()
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
    public async Task UpdateOwner_Success()
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
    public async Task UpdateOwner_ReturnsNullWhenNotFound()
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
    public async Task DeleteOwner_Success()
    {
        await _ownerrepoMock.DeleteOwner(1);
        var result = await _ownerrepoMock.GetOwner(1);
        Assert.Null(result);
    }

    [Fact]
    public async Task IfOwnerExists_Success()
    {
        var result = _ownerrepoMock.IfOwnerExists(1);
        Assert.True(result);
    }

    [Fact]
    public async Task IfOwnerDoesNotExists_Fail()
    {
        var result = _ownerrepoMock.IfOwnerExists(935231311);
        Assert.False(result);
    }
}