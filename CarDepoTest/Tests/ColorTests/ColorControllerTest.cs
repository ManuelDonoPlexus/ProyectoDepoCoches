using CarDepo.API.Models;
using Moq;
using CarDepo.Infrastructure.Repositories;
using CarDepo.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using CarDepo.Application.Services;
using CarDepo.CarDepoTest.Tests.Utils;

namespace CarDepo.CarDepoTest.Tests.ColorTests;

public class ColorControllerTest
{
    private readonly Mock<ColorRepository> _colorrepoMock;
    private readonly Mock<ColorService> _colorserviceMock;
    private readonly ColorController _colorcontroller;

    public ColorControllerTest()
    {
        _colorrepoMock = new Mock<ColorRepository>(new RepoClassTestUtil().ReturnFakeContext());
        _colorserviceMock = new Mock<ColorService>(_colorrepoMock.Object);
        _colorcontroller = new ColorController(_colorserviceMock.Object);
    }

    [Fact]
    public async Task GetAllColors_ReturnsOk()
    {
        Color testcolor = new Color
        {
            Id = 1,
            Name = "Azul"
        };

        var result = await _colorcontroller.GetAllColors();

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task CreateColor_ReturnsBadRequestWhenNull()
    {
        var result = await _colorcontroller.CreateColor(null);

        Assert.IsType<BadRequestResult>(result?.Result);
    }

    [Fact]
    public async Task ModifyColor_ReturnsOk()
    {
        Color testcolor = new Color
        {
            Name = "Violeta"
        };

        var result = await _colorcontroller.ModifyColor(1, testcolor);

        Assert.IsType<OkObjectResult>(result);
    }
}