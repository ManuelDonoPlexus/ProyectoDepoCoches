using CarDepo.API.Models;
using Moq;
using CarDepo.Infrastructure.Repositories;
using CarDepo.Application.Services;
using CarDepo.API.DTOs.Driver;

namespace CarDepo.CarDepoTest.Tests.DriverTests;

public class DriverServiceTest
{
    private readonly Mock<DriverRepository> _driverrepoMock;
    private readonly DriverService _driverserviceMock;

    public DriverServiceTest()
    {
        _driverrepoMock = new Mock<DriverRepository>();
        _driverserviceMock = new DriverService(_driverrepoMock.Object);
    }

    [Fact]
    public async Task InsertDriver_ReturnsInsertedDriver()
    {
        Driver testdriver = new Driver
        {
            Id = 1,
            Name = "Pepe",
            Dni = "11111111A",
            EmailAddr = "pepe@mail.com",
            PhoneNumber = 123456789,
            OwnerId = 1,
        };

        var cardriverDTO = new DriverDTO
        {
            Id = testdriver.Id,
            Name = testdriver.Name,
            Dni = testdriver.Dni,
            EmailAddr = testdriver.EmailAddr,
            PhoneNumber = 987654321,
            OwnerId = testdriver.OwnerId,
        };

        _driverrepoMock.Setup(r => r.InsertDriver(testdriver))
            .ReturnsAsync(cardriverDTO);
        
        var result = await _driverserviceMock.InsertDriver(testdriver);

        Assert.NotNull(result);
        Assert.Equal(testdriver.Name, result.Name);
    }
}