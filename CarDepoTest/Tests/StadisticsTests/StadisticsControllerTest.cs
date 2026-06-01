using CarDepo.API.Models;
using Moq;
using CarDepo.Infrastructure.Repositories;
using CarDepo.API.Controllers;
using CarDepo.Application.Services;
using System.Collections;

namespace CarDepo.CarDepoTest.Tests.StadisticsTests;

public class StadisticsControllerTest
{
    private readonly Mock<StadisticsRepository> _stadisticrepoMock;
    private readonly Mock<StadisticsService> _stadisticserviceMock;
    private readonly StadisticsController _stadisticcontroller;

    public StadisticsControllerTest()
    {
        _stadisticrepoMock = new Mock<StadisticsRepository>();
        _stadisticserviceMock = new Mock<StadisticsService>(_stadisticrepoMock.Object);
        _stadisticcontroller = new StadisticsController(_stadisticserviceMock.Object);
    }

    [Fact]
    public async Task GetCarKmsAvg_ReturnsOk()
    {
        Car testcar1 = new Car
        {
            Id = 1,
            License = "1234-ABC",
            Kms = 100,
            ColorId = 1,
            MakeId = 1,
            OwnerId = 1
        };
        
        Car testcar2 = new Car
        {
            Id = 2,
            License = "5678-DEF",
            Kms = 200,
            ColorId = 1,
            MakeId = 1,
            OwnerId = 1
        };

        Car testcar3 = new Car
        {
            Id = 3,
            License = "9101112-GHI",
            Kms = 300,
            ColorId = 1,
            MakeId = 1,
            OwnerId = 1
        };

        var cars = new List<Car>(){testcar1, testcar2, testcar3};
        var value = cars.Average(c => c.Kms);

        _stadisticrepoMock.Setup(r => r.GetCarKms())
            .ReturnsAsync(value);

        var result = await _stadisticcontroller.GetCarKms();

        Assert.IsType<double>(result);
    }

    [Fact]
    public async Task GetAvgPrice_ReturnsOk()
    {
        Make testmake1 = new Make
        {
            Id = 1,
            Name = "Seat",
            HorsePower = 100,
            Price = 1000.00M,
            FuelTypeId = 1
        };

        Make testmake2 = new Make
        {
            Id = 2,
            Name = "Fiat",
            HorsePower = 200,
            Price = 1500.50M,
            FuelTypeId = 1
        };

        Make testmake3 = new Make
        {
            Id = 3,
            Name = "Mercedes",
            HorsePower = 300,
            Price = 2110.11M,
            FuelTypeId = 1
        };

        var makes = new List<Make>(){testmake1, testmake2, testmake3};
        var avg = makes.Average(m => m.Price);

        _stadisticrepoMock.Setup(r => r.GetAveragePrice())
            .ReturnsAsync(avg);

        var result = await _stadisticcontroller.GetAveragePrice();

        Assert.IsType<decimal>(result);
    }

    [Fact]
    public async Task GetColorCount_ReturnsOk()
    {
        Car testcar1 = new Car
        {
            Id = 1,
            License = "1234-ABC",
            Kms = 100,
            ColorId = 1,
            MakeId = 1,
            OwnerId = 1
        };
        
        Car testcar2 = new Car
        {
            Id = 2,
            License = "5678-DEF",
            Kms = 200,
            ColorId = 1,
            MakeId = 1,
            OwnerId = 1
        };

        Car testcar3 = new Car
        {
            Id = 3,
            License = "9101112-GHI",
            Kms = 300,
            ColorId = 1,
            MakeId = 1,
            OwnerId = 1
        };

        var cars = new List<Car>(){testcar1, testcar2, testcar3};
        var count = cars.CountBy(c => c.ColorId);

        _stadisticrepoMock.Setup(r=> r.GetCarColorCount())
            .ReturnsAsync(count);

        var result = await _stadisticcontroller.GetColorCount();
        await Assert.IsType<Task<IEnumerable>>(result);
    }

    [Fact]
    public async Task GetMakeCount_ReturnsOk()
    {
        Car testcar1 = new Car
        {
            Id = 1,
            License = "1234-ABC",
            Kms = 100,
            ColorId = 1,
            MakeId = 1,
            OwnerId = 1
        };
        
        Car testcar2 = new Car
        {
            Id = 2,
            License = "5678-DEF",
            Kms = 200,
            ColorId = 1,
            MakeId = 1,
            OwnerId = 1
        };

        Car testcar3 = new Car
        {
            Id = 3,
            License = "9101112-GHI",
            Kms = 300,
            ColorId = 1,
            MakeId = 1,
            OwnerId = 1
        };

        var cars = new List<Car>(){testcar1, testcar2, testcar3};
        var count = cars.CountBy(c => c.MakeId);

        _stadisticrepoMock.Setup(r=> r.GetCarMakeCount())
            .ReturnsAsync(count);

        var result = await _stadisticcontroller.GetColorCount();
        await Assert.IsType<Task<IEnumerable>>(result);
    }

    [Fact]
    public async Task GetOwnerCount_ReturnsOk()
    {
        Car testcar1 = new Car
        {
            Id = 1,
            License = "1234-ABC",
            Kms = 100,
            ColorId = 1,
            MakeId = 1,
            OwnerId = 1
        };
        
        Car testcar2 = new Car
        {
            Id = 2,
            License = "5678-DEF",
            Kms = 200,
            ColorId = 1,
            MakeId = 1,
            OwnerId = 1
        };

        Car testcar3 = new Car
        {
            Id = 3,
            License = "9101112-GHI",
            Kms = 300,
            ColorId = 1,
            MakeId = 1,
            OwnerId = 1
        };

        var cars = new List<Car>(){testcar1, testcar2, testcar3};
        var count = cars.CountBy(c => c.OwnerId);

        _stadisticrepoMock.Setup(r=> r.GetCarOwnerCount())
            .ReturnsAsync(count);

        var result = await _stadisticcontroller.GetColorCount();
        await Assert.IsType<Task<IEnumerable>>(result);
    }

    [Fact]
    public async Task GetCarDriverCount_ReturnsOk()
    {
        CarDriver testcardriver1 = new CarDriver
        {
            DateDrive = DateOnly.Parse("2011-01-10"),
            CarCDId = 1,
            DriverCDId = 1
        };
        CarDriver testcardriver2 = new CarDriver
        {
            DateDrive = DateOnly.Parse("2012-02-20"),
            CarCDId = 2,
            DriverCDId = 2
        };
        CarDriver testcardriver3 = new CarDriver
        {
            DateDrive = DateOnly.Parse("2013-03-30"),
            CarCDId = 3,
            DriverCDId = 3
        };

        var cds = new List<CarDriver>(){testcardriver1,testcardriver2,testcardriver3};
        var count = cds.CountBy(c => c.CarCDId);

        _stadisticrepoMock.Setup(r => r.GetCarDriverAssociatedCarCount())
            .ReturnsAsync(count);
        
        var result = await _stadisticcontroller.GetAssociatedCarCount();
        await Assert.IsType<Task<IEnumerable>>(result);
    }

}