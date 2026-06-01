using CarDepo.API.Models;
using Moq;
using CarDepo.Infrastructure.Repositories;
using CarDepo.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using CarDepo.Application.Services;
using CarDepo.API.DTOs.Color;

namespace CarDepo.CarDepoTest.Tests.ColorTests;

public class ColorControllerTest
{
    private readonly Mock<ColorRepository> _colorrepoMock;
    private readonly Mock<ColorService> _colorserviceMock;
    private readonly ColorController _colorcontroller;

    public ColorControllerTest()
    {
        _colorrepoMock = new Mock<ColorRepository>();
        _colorserviceMock = new Mock<ColorService>(_colorrepoMock.Object);
        _colorcontroller = new ColorController(_colorserviceMock.Object);
    }

    [Fact]
    public async Task GetAllCars_ReturnsOk()
    {
        Color testcolor = new Color
        {
            Id = 1,
            Name = "Azul"
        };

        var colors = new List<Color>(){testcolor};

        _colorrepoMock.Setup(r => r.GetColors())
            .ReturnsAsync(colors);

        var result = await _colorcontroller.GetAllColors();

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task CreateCar_ReturnsBadRequestWhenNull()
    {
        _colorrepoMock.Setup(r => r.InsertColor(null))
            .Returns((Task<ColorDTO?>)null);
        
        var result = await _colorcontroller.CreateColor(null);

        Assert.IsType<BadRequestResult>(result?.Result);
    }

    [Fact]
    public async Task ModifyCar_ReturnsOk()
    {
        Color testcolor = new Color
        {
            Id = 1,
            Name = "Azul"
        };

        var colorDTO = new ColorDTO
        {
            Id = 1,
            Name = "Rojo"
        };

        _colorrepoMock.Setup(r => r.UpdateColor(1, testcolor))
            .ReturnsAsync(colorDTO);

        var result = await _colorcontroller.GetAllColors();

        Assert.IsType<OkObjectResult>(result);
    }
}