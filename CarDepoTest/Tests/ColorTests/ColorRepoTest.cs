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
    public async Task GetColors_ReturnsCars()
    {
        var result = await _colorrepoMock.GetColors();

        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetColor_ReturnsDTOWhenExists()
    {
        var result = await _colorrepoMock.GetColor(1);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetColor_ReturnsNullWhenNotExists()
    {
        var result = await _colorrepoMock.GetColor(362562);

        Assert.Null(result);
    }

    [Fact]
    public async Task InsertColor_Success()
    {
        Color testcolor = new Color
        {
            Name = "Azul"
        };

        var result = await _colorrepoMock.InsertColor(testcolor);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateColor_Success()
    {
        Color testcolor = new Color
        {
            Name = "Azul"
        };

        var result = await _colorrepoMock.UpdateColor(1,testcolor);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateColor_ReturnsNullWhenNotFound()
    {
        Color testcolor = new Color
        {
            Name = "Azul"
        };

        var result = await _colorrepoMock.UpdateColor(9461232,testcolor);

        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteColor_Success()
    {
        await _colorrepoMock.DeleteColor(1);

        var result = await _colorrepoMock.GetColor(1);

        Assert.Null(result);
    }

    [Fact]
    public async Task IfColorExists_Success()
    {

        var result = _colorrepoMock.IfColorExists(1);

        Assert.True(result);
    }

    [Fact]
    public async Task IfColorDoesNotExists_Fail()
    {
        var result = _colorrepoMock.IfColorExists(935231311);

        Assert.False(result);
    }
}