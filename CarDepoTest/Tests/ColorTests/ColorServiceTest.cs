using CarDepo.API.Models;
using Moq;
using CarDepo.Infrastructure.Repositories;
using CarDepo.Application.Services;
using CarDepo.API.DTOs.Color;

namespace CarDepo.CarDepoTest.Tests.ColorTests;

public class ColorServiceTest
{
    private readonly Mock<ColorRepository> _colorrepoMock;
    private readonly ColorService _colorserviceMock;

    public ColorServiceTest()
    {
        _colorrepoMock = new Mock<ColorRepository>();
        _colorserviceMock = new ColorService(_colorrepoMock.Object);
    }

    [Fact]
    public async Task InsertCar_ReturnsInsertedCar()
    {
        Color testcolor = new Color
        {
            Id = 1,
            Name = "Azul"
        };

        var colorDTO = new ColorDTO
        {
            Id = 1,
            Name = "Azul"
        };

        _colorrepoMock.Setup(r => r.InsertColor(testcolor))
            .ReturnsAsync(colorDTO);
        
        var result = await _colorserviceMock.InsertColor(testcolor);

        Assert.NotNull(result);
        Assert.Equal(testcolor.Name, result.Name);
    }
}