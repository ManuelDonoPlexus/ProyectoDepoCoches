using CarDepo.API.Models;
using Moq;
using CarDepo.Infrastructure.Repositories;
using CarDepo.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using CarDepo.Application.Services;

namespace CarDepo.CarDepoTest;

public class CarControllerTest
{

    [Fact]
    public async Task GetSpecificCar_Success()
    {
        Mock<ICarRepository> _mockcarRepo = new Mock<ICarRepository>();
        Mock<CarService> _mockcarService = new Mock<CarService>(_mockcarRepo.Object);
        CarController _carController = new CarController(_mockcarService.Object);

        int id = 1;

        ActionResult<Car?> result = await _carController.GetSpecificCar(id);

        Assert.NotNull(result);
        Assert.IsType<ActionResult<Car?>>(result);
    }

    [Fact]
    public async Task CreateCar_Success()
    {
        Mock<ICarRepository> _mockcarRepo = new Mock<ICarRepository>();
        Mock<CarService> _mockcarService = new Mock<CarService>(_mockcarRepo.Object);
        CarController _carController = new CarController(_mockcarService.Object);

        Car car = new Car
        {
            License = "ABCD-123",
            Kms = 123,
            ColorId = 1,
            OwnerId = 1,
            MakeId = 1
        };

        ActionResult<Car?> result = await _carController.CreateCar(car);

        Assert.NotNull(result);
        Assert.IsType<ActionResult<Car>>(result);
    }

    [Fact]
    public async Task UpdateCar_NotFound()
    {
        Mock<ICarRepository> _mockcarRepo = new Mock<ICarRepository>();
        Mock<CarService> _mockcarService = new Mock<CarService>(_mockcarRepo.Object);
        CarController _carController = new CarController(_mockcarService.Object);

        Car car2 = new Car
        {
            License = "ABCD-123",
            Kms = 200,
            ColorId = -3,
            OwnerId = -3,
            MakeId = -3
        };

        IActionResult result = await _carController.ModifyCar(1, car2);

        Assert.NotNull(result);
        Assert.IsType<BadRequestResult>(result);
        
    }

    [Fact]
    public async Task DeleteCar_NotFound()
    {
        Mock<ICarRepository> _mockcarRepo = new Mock<ICarRepository>();
        Mock<CarService> _mockcarService = new Mock<CarService>(_mockcarRepo.Object);
        CarController _carController = new CarController(_mockcarService.Object);

        IActionResult result = await _carController.DeleteCar(1);

        Assert.NotNull(result);
        Assert.IsType<NotFoundResult>(result);
    }
}