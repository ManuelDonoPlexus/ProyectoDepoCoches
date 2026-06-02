using CarDepo.API.Models;
using Moq;
using CarDepo.Infrastructure.Repositories;
using CarDepo.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using CarDepo.Application.Services;
using CarDepo.API.DTOs.Fine;
using CarDepo.CarDepoTest.Tests.Utils;

namespace CarDepoTest.Tests.FineTests;

public class FineControllerTest
{
    private readonly Mock<FineRepository> _finerepoMock;
    private readonly Mock<FineService> _fineserviceMock;
    private readonly FineController _finecontroller;

    public FineControllerTest()
    {
        _finerepoMock = new Mock<FineRepository>(new RepoClassTestUtil().ReturnFakeContext());
        _fineserviceMock = new Mock<FineService>(_finerepoMock.Object);
        _finecontroller = new FineController(_fineserviceMock.Object);
    }

    [Fact]
    public async Task GetAllFines_ReturnsOk()
    {
        var result = await _finecontroller.GetAllFines();

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task CreateFine_ReturnsBadRequestWhenNull()
    {
        var result = await _finecontroller.CreateFine(null);

        Assert.IsType<BadRequestResult>(result?.Result);
    }

    [Fact]
    public async Task ModifyFine_ReturnsOk()
    {
        Fine testfine = new Fine
        {
            Price = 100.00M,
            Payed = true,
            Description = "",
            Date = DateOnly.Parse("2001-01-1"),
            OwnerId = 1,
            CarId = 1
        };

        var result = await _finecontroller.ModifyFine(1, testfine);

        Assert.IsType<OkObjectResult>(result);
    }
}