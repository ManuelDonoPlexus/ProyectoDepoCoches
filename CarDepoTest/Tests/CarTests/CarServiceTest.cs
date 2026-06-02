using CarDepo.API.Models;
using Moq;
using CarDepo.Infrastructure.Repositories;
using CarDepo.Application.Services;
using CarDepo.API.DTOs.Car;
using CarDepo.CarDepoTest.Tests.Utils;

namespace CarDepo.CarDepoTest.Tests.CarTests;

public class CarServiceTest
{
    private readonly Mock<CarRepository> _carrepoMock;
    private readonly CarService _carserviceMock;

    public CarServiceTest()
    {
        _carrepoMock = new Mock<CarRepository>(new RepoClassTestUtil().ReturnFakeContext());
        _carserviceMock = new CarService(_carrepoMock.Object);
    }

    [Fact]
    public async Task InsertCar_ReturnsInsertedCar()
    {
        Car testcar = new Car
        {
            License = "1234-ABC",
            Kms = 100,
            ColorId = 1,
            MakeId = 1,
            OwnerId = 1
        };
        
        var result = await _carserviceMock.InsertCar(testcar);

        Assert.NotNull(result);
        Assert.Equal(testcar.License, result.License);
    }
}