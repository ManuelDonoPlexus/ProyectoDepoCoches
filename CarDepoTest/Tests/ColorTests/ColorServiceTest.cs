using CarDepo.API.Models;
using Moq;
using CarDepo.Infrastructure.Repositories;
using CarDepo.Application.Services;
using CarDepo.API.DTOs.Color;
using CarDepo.CarDepoTest.Tests.Utils;

namespace CarDepoTest.Tests.ColorTests;

public class ColorServiceTest
{
    private readonly Mock<ColorRepository> _colorrepoMock;
    private readonly ColorService _colorserviceMock;

    public ColorServiceTest()
    {
        _colorrepoMock = new Mock<ColorRepository>(new RepoClassTestUtil().ReturnFakeContext());
        _colorserviceMock = new ColorService(_colorrepoMock.Object);
    }

    [Fact]
    public async Task InsertColor_ReturnsInsertedCar()
    {
        Color testcolor = new Color
        {
            Name = "Verde"
        };

        var colorDTO = new ColorDTO
        {
            Name = "Violeta"
        };
        
        var result = await _colorserviceMock.InsertColor(testcolor);

        Assert.NotNull(result);
        Assert.Equal(testcolor.Name, result.Name);
    }
}