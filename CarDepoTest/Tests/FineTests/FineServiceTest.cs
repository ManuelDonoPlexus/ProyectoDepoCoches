using CarDepo.API.Models;
using Moq;
using CarDepo.Infrastructure.Repositories;
using CarDepo.Application.Services;
using CarDepo.API.DTOs.Fine;

namespace CarDepo.CarDepoTest.Tests.FineTests;

public class FineServiceTest
{
    private readonly Mock<FineRepository> _finerepoMock;
    private readonly FineService _fineserviceMock;

    public FineServiceTest()
    {
        _finerepoMock = new Mock<FineRepository>();
        _fineserviceMock = new FineService(_finerepoMock.Object);
    }

    [Fact]
    public async Task InsertFine_ReturnsInsertedDriver()
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

        var fineDTO = new FineDTO
        {
            Id = testfine.Id,
            Price = testfine.Price,
            Payed = true,
            Description = testfine.Description,
            Date = testfine.Date,
            OwnerId = testfine.OwnerId,
            CarId = testfine.CarId
        };

        _finerepoMock.Setup(r => r.InsertFine(testfine))
            .ReturnsAsync(fineDTO);
        
        var result = await _fineserviceMock.InsertFine(testfine);

        Assert.NotNull(result);
        Assert.Equal(testfine.Id, result.Id);
    }
}