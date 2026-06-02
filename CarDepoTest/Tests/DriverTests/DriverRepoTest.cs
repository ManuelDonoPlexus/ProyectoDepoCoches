using CarDepo.API.Models;
using CarDepo.Infrastructure.Repositories;
using CarDepo.CarDepoTest.Tests.Utils;

namespace CarDepoTest.Tests.DriverTests;

public class DriverRepoTest
{
    private readonly DriverRepository _driverrepoMock;

    public DriverRepoTest(){
        _driverrepoMock = new DriverRepository(new RepoClassTestUtil().ReturnFakeContext());
    }

    [Fact]
    public async Task GetDrivers_ReturnsDrivers()
    {
        var result = await _driverrepoMock.GetDrivers();

        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetDriver_ReturnsDTOWhenExists()
    {
        var result = await _driverrepoMock.GetDriver(1);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetDriver_ReturnsNullWhenNotExists()
    {
        var result = await _driverrepoMock.GetDriver(362562);

        Assert.Null(result);
    }

    [Fact]
    public async Task InsertDriver_Success()
    {
        Driver testdriver = new Driver
        {
            Id = 2,
            Name = "Pepe",
            Dni = "11111111A",
            EmailAddr = "pepe@mail.com",
            PhoneNumber = 123456789,
            OwnerId = 1,
        };

        var result = await _driverrepoMock.InsertDriver(testdriver);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateDriver_Success()
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

        var result = await _driverrepoMock.UpdateDriver(1,testdriver);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateDriver_ReturnsNullWhenNotFound()
    {
        Driver testdriver = new Driver
        {
            Id = 9461232,
            Name = "Pepe",
            Dni = "11111111A",
            EmailAddr = "pepe@mail.com",
            PhoneNumber = 123456789,
            OwnerId = 1,
        };

        var result = await _driverrepoMock.UpdateDriver(9461232,testdriver);

        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteDriver_Success()
    {
        await _driverrepoMock.DeleteDriver(1);

        var result = await _driverrepoMock.GetDriver(1);

        Assert.Null(result);
    }

    [Fact]
    public async Task IfDriverExists_Success()
    {

        var result = _driverrepoMock.IfDriverExists(1);

        Assert.True(result);
    }

    [Fact]
    public async Task IfDriverDoesNotExists_Fail()
    {
        var result = _driverrepoMock.IfDriverExists(935231311);

        Assert.False(result);
    }
}