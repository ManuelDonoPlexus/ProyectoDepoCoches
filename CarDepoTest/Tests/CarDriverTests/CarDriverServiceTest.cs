using CarDepo.API.Models;
using Moq;
using CarDepo.Infrastructure.Repositories;
using CarDepo.Application.Services;
using CarDepo.API.DTOs.CarDriver;

namespace CarDepo.CarDepoTest.Tests.CarTests;

public class CarDriverServiceTest
{
    private readonly Mock<CarDriverRepository> _cardriverrepoMock;
    private readonly CarDriverService _cardriverserviceMock;

    public CarDriverServiceTest()
    {
        _cardriverrepoMock = new Mock<CarDriverRepository>();
        _cardriverserviceMock = new CarDriverService(_cardriverrepoMock.Object);
    }

    [Fact]
    public async Task InsertCarDriver_ReturnsInsertedCarDriver()
    {
        CarDriver testcardriver = new CarDriver
        {
            Id = 1,
            DateDrive = DateOnly.Parse("2001-01-1"),
            CarCDId = 1,
            DriverCDId = 1
        };

        var cardriverDTO = new CarDriverDTO
        {
            Id = testcardriver.Id,
            DateDrive = testcardriver.DateDrive,
            CarCDId = testcardriver.CarCDId,
            DriverCDId = testcardriver.DriverCDId
        };

        _cardriverrepoMock.Setup(r => r.InsertCarDriver(testcardriver))
            .ReturnsAsync(cardriverDTO);
        
        var result = await _cardriverserviceMock.InsertCarDriver(testcardriver);

        Assert.NotNull(result);
        Assert.Equal(testcardriver.DateDrive, result.DateDrive);
    }
}