using CarDepo.API.Models;
using Moq;
using CarDepo.Infrastructure.Repositories;
using CarDepo.Application.Services;
using CarDepo.API.DTOs.Fine;
using CarDepo.CarDepoTest.Tests.Utils;

namespace CarDepo.CarDepoTest.Tests.FineTests;

public class FineServiceTest
{
    private readonly Mock<FineRepository> _finerepoMock;
    private readonly FineService _fineserviceMock;

    public FineServiceTest()
    {
        _finerepoMock = new Mock<FineRepository>(new RepoClassTestUtil().ReturnFakeContext());
        _fineserviceMock = new FineService(_finerepoMock.Object);
    }

    [Fact]
    public async Task InsertFine_ReturnsInsertedFine()
    {
        Fine testfine = new Fine
        {
            Price = 100.00M,
            Payed = true,
            Description = "",
            Date = DateOnly.Parse("2001-01-1"),
            OwnerId = 1,
            CarId = 1
        };
        
        var result = await _fineserviceMock.InsertFine(testfine);

        Assert.NotNull(result);
        Assert.Equal(testfine.Id, result.Id);
    }
}