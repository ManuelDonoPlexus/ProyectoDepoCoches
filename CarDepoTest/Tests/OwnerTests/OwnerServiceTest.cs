using CarDepo.API.Models;
using Moq;
using CarDepo.Infrastructure.Repositories;
using CarDepo.Application.Services;
using CarDepo.CarDepoTest.Tests.Utils;

namespace CarDepoTest.Tests.OwnerTests;

public class OwnerServiceTest
{
    private readonly Mock<OwnerRepository> _ownerrepoMock;
    private readonly OwnerService _ownerserviceMock;

    public OwnerServiceTest()
    {
        _ownerrepoMock = new Mock<OwnerRepository>(new RepoClassTestUtil().ReturnFakeContext());
        _ownerserviceMock = new OwnerService(_ownerrepoMock.Object);
    }

    [Fact]
    public async Task InsertOwner_ReturnsInsertedOwner()
    {
        Owner testowner = new Owner
        {
            Name = "Test INC",
            Nif = "A11113333",
            PhoneNumber = 999222111,
            DateEntry = DateOnly.Parse("2001-01-01"),
            EmailAddr = "test@mail"
        };
        
        var result = await _ownerserviceMock.InsertOwner(testowner);

        Assert.NotNull(result);
        Assert.Equal(testowner.Id, result.Id);
    }
}