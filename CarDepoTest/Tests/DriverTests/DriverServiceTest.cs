using CarDepo.API.Models;
using Moq;
using CarDepo.Infrastructure.Repositories;
using CarDepo.Application.Services;
using CarDepo.CarDepoTest.Tests.Utils;

namespace CarDepo.CarDepoTest.Tests.DriverTests;

public class DriverServiceTest
{
    private readonly Mock<DriverRepository> _driverrepoMock;
    private readonly DriverService _driverserviceMock;

    public DriverServiceTest()
    {
        _driverrepoMock = new Mock<DriverRepository>(new RepoClassTestUtil().ReturnFakeContext());
        _driverserviceMock = new DriverService(_driverrepoMock.Object);
    }

    [Fact]
    public async Task InsertDriver_ReturnsInsertedDriver()
    {
        Driver testdriver = new Driver
        {
            Name = "Pepe",
            Dni = "11111111A",
            EmailAddr = "pepe@mail.com",
            PhoneNumber = 123456789,
            OwnerId = 1,
        };
        
        var result = await _driverserviceMock.InsertDriver(testdriver);

        Assert.NotNull(result);
        Assert.Equal(testdriver.Name, result.Name);
    }
}