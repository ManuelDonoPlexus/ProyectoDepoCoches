using CarDepo.API.Models;
using Moq;
using CarDepo.Infrastructure.Repositories;
using CarDepo.Application.Services;
using CarDepo.API.DTOs.Make;
using CarDepo.API.DTOs.Owner;

namespace CarDepo.CarDepoTest.Tests.MakeTests;

public class OwnerServiceTest
{
    private readonly Mock<OwnerRepository> _ownerrepoMock;
    private readonly OwnerService _ownerserviceMock;

    public OwnerServiceTest()
    {
        _ownerrepoMock = new Mock<OwnerRepository>();
        _ownerserviceMock = new OwnerService(_ownerrepoMock.Object);
    }

    [Fact]
    public async Task InsertFuelType_ReturnsInsertedDriver()
    {
        Owner testowner = new Owner
        {
            Id = 1,
            Name = "Test INC",
            Nif = "A11113333",
            PhoneNumber = 999222111,
            DateEntry = DateOnly.Parse("2001-01-01"),
            EmailAddr = "test@mail"
        };

        var ownerDTO = new OwnerDTO
        {
            Id = testowner.Id,
            Name = testowner.Name,
            Nif = testowner.Nif,
            PhoneNumber = 923145678,
            DateEntry = testowner.DateEntry,
            EmailAddr = testowner.EmailAddr
        };

        _ownerrepoMock.Setup(r => r.InsertOwner(testowner))
            .ReturnsAsync(ownerDTO);
        
        var result = await _ownerserviceMock.InsertOwner(testowner);

        Assert.NotNull(result);
        Assert.Equal(testowner.Id, result.Id);
    }
}