using CarDepo.API.Models;
using Moq;
using CarDepo.Infrastructure.Repositories;
using CarDepo.Application.Services;
using CarDepo.CarDepoTest.Tests.Utils;

namespace CarDepo.CarDepoTest.Tests.CarDriverTests;

public class CarDriverServiceTest
{
    private readonly Mock<CarDriverRepository> _cardriverrepoMock;
    private readonly CarDriverService _cardriverserviceMock;

    public CarDriverServiceTest()
    {
        _cardriverrepoMock = new Mock<CarDriverRepository>(new RepoClassTestUtil().ReturnFakeContext());
        _cardriverserviceMock = new CarDriverService(_cardriverrepoMock.Object);
    }

    [Fact]
    public async Task InsertCarDriver_ReturnsInsertedCarDriver()
    {
        CarDriver testcardriver = new CarDriver
        {
            DateDrive = DateOnly.Parse("2001-01-1"),
            CarCDId = 1,
            DriverCDId = 1
        };
        
        var result = await _cardriverserviceMock.InsertCarDriver(testcardriver);

        Assert.NotNull(result);
        Assert.Equal(testcardriver.DateDrive, result.DateDrive);
    }
}