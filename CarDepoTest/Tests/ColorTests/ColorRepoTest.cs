using CarDepo.API.Models;
using Moq;
using CarDepo.Infrastructure.Repositories;
using CarDepo.CarDepoTest.Tests.Utils;

namespace CarDepo.CarDepoTest.Tests.ColorTests;

public class ColorRepoTest
{
    private readonly ColorRepository _colorrepoMock;

    public ColorRepoTest(){
        _colorrepoMock = new ColorRepository(new RepoClassTestUtil().ReturnFakeContext());
    }

    [Fact]
    public async Task GetCars_ReturnsCars()
    {
        var result = await _colorrepoMock.GetColors();

        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetCar_ReturnsDTOWhenExists()
    {
        var result = await _colorrepoMock.GetColor(1);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetCar_ReturnsNullWhenNotExists()
    {
        var result = await _colorrepoMock.GetColor(362562);

        Assert.Null(result);
    }

    [Fact]
    public async Task InsertCar_Success()
    {
        Color testcolor = new Color
        {
            Id = 2,
            Name = "Azul"
        };

        var result = await _colorrepoMock.InsertColor(testcolor);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateCar_Success()
    {
        Color testcolor = new Color
        {
            Id = 2,
            Name = "Azul"
        };

        var result = await _colorrepoMock.UpdateColor(2,testcolor);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateCar_ReturnsNullWhenNotFound()
    {
        Color testcolor = new Color
        {
            Id = 9461232,
            Name = "Azul"
        };

        var result = await _colorrepoMock.UpdateColor(9461232,testcolor);

        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteCar_Success()
    {
        await _colorrepoMock.DeleteColor(1);

        var result = await _colorrepoMock.GetColor(1);

        Assert.Null(result);
    }

    [Fact]
    public async Task IfCarExists_Success()
    {

        var result = _colorrepoMock.IfColorExists(1);

        Assert.True(result);
    }

    [Fact]
    public async Task IfCarDoesNotExists_Fail()
    {
        var result = _colorrepoMock.IfColorExists(935231311);

        Assert.False(result);
    }
}