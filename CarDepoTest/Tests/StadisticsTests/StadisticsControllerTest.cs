using Moq;
using CarDepo.Infrastructure.Repositories;
using CarDepo.API.Controllers;
using CarDepo.Application.Services;
using System.Collections;
using CarDepoTest.Tests.Utils;

namespace CarDepoTest.Tests.StadisticsTests;

public class StadisticsControllerTest
{
    private readonly Mock<StadisticsRepository> _stadisticrepoMock;
    private readonly Mock<StadisticsService> _stadisticserviceMock;
    private readonly StadisticsController _stadisticcontroller;

    public StadisticsControllerTest()
    {
        _stadisticrepoMock = new Mock<StadisticsRepository>(new RepoClassTestUtil().ReturnFakeContext());
        _stadisticserviceMock = new Mock<StadisticsService>(_stadisticrepoMock.Object);
        _stadisticcontroller = new StadisticsController(_stadisticserviceMock.Object);
    }

    [Fact]
    public async Task GetCarKmsAvg_ReturnsOk()
    {
        var result = await _stadisticcontroller.GetCarKms();

        Assert.IsType<double>(result);
    }

    [Fact]
    public async Task GetAvgPrice_ReturnsOk()
    {
        var result = await _stadisticcontroller.GetAveragePrice();

        Assert.IsType<decimal>(result);
    }

    [Fact]
    public async Task GetColorCount_ReturnsOk()
    {
        var result = await _stadisticcontroller.GetColorCount();
        await Assert.IsType<Task<IEnumerable?>>(result);
    }

    [Fact]
    public async Task GetMakeCount_ReturnsOk()
    {
        var result = await _stadisticcontroller.GetColorCount();
        await Assert.IsType<Task<IEnumerable?>>(result);
    }

    [Fact]
    public async Task GetOwnerCount_ReturnsOk()
    {
        var result = await _stadisticcontroller.GetColorCount();
        await Assert.IsType<Task<IEnumerable?>>(result);
    }

    [Fact]
    public async Task GetCarDriverCount_ReturnsOk()
    {
        var result = await _stadisticcontroller.GetAssociatedCarCount();
        await Assert.IsType<Task<IEnumerable?>>(result);
    }

}