using CarDepo.API.Models;
using Moq;
using CarDepo.Infrastructure.Repositories;
using CarDepo.Application.Services;
using CarDepo.API.DTOs.Make;
using CarDepo.CarDepoTest.Tests.Utils;

namespace CarDepo.CarDepoTest.Tests.MakeTests;

public class FuelTypeServiceTest
{
    private readonly Mock<MakeRepository> _makerepoMock;
    private readonly MakeService _makeserviceMock;

    public FuelTypeServiceTest()
    {
        _makerepoMock = new Mock<MakeRepository>(new RepoClassTestUtil().ReturnFakeContext());
        _makeserviceMock = new MakeService(_makerepoMock.Object);
    }

    [Fact]
    public async Task InsertMake_ReturnsInsertedMake()
    {
        Make testmake = new Make
        {
            Name = "Seat",
            HorsePower = 100,
            Price = 1000.00M,
            FuelTypeId = 1
        };
        
        var result = await _makeserviceMock.InsertMake(testmake);

        Assert.NotNull(result);
        Assert.Equal(testmake.Id, result.Id);
    }
}