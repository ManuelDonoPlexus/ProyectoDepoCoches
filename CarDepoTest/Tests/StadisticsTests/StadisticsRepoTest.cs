using CarDepoTest.Tests.Utils;
using CarDepo.Infrastructure.Repositories;

namespace CarDepoTest.Tests.StadisticsTests;

public class StadisticsRepoTest
{
    private readonly StadisticsRepository _stadisticrepoMock;
    public StadisticsRepoTest() {
        _stadisticrepoMock = new StadisticsRepository(new RepoClassTestUtil().ReturnFakeContext());
    }

    [Fact]
    public async Task GetCarKmsAvg()
    {
        var result = await _stadisticrepoMock.GetCarKms();
        Assert.IsType<double>(result);
    }

    [Fact]
    public async Task GetMakePriceAvg()
    {
        var result = await _stadisticrepoMock.GetAveragePrice();
        Assert.IsType<decimal>(result);
    }
    
    [Fact]
    public async Task GetCarColorCount()
    {
        var result = await _stadisticrepoMock.GetCarColorCount();
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetCarMakeCount()
    {
        var result = await _stadisticrepoMock.GetCarMakeCount();
        Assert.NotNull(result);
    }
    
    [Fact]
    public async Task GetCarOwnerAvg()
    {
        var result = await _stadisticrepoMock.GetCarOwnerCount();
        Assert.NotNull(result);
    }
    
    [Fact]
    public async Task GetCarDriverCount()
    {
        var result = await _stadisticrepoMock.GetCarDriverAssociatedCarCount();
        Assert.NotNull(result);
    }
}