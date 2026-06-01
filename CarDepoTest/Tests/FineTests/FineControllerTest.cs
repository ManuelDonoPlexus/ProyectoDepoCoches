using CarDepo.API.Models;
using Moq;
using CarDepo.Infrastructure.Repositories;
using CarDepo.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using CarDepo.Application.Services;
using CarDepo.API.DTOs.Fine;

namespace CarDepo.CarDepoTest.Tests.FineTests;

public class FineControllerTest
{
    private readonly Mock<FineRepository> _finerepoMock;
    private readonly Mock<FineService> _fineserviceMock;
    private readonly FineController _finecontroller;

    public FineControllerTest()
    {
        _finerepoMock = new Mock<FineRepository>();
        _fineserviceMock = new Mock<FineService>(_finerepoMock.Object);
        _finecontroller = new FineController(_fineserviceMock.Object);
    }

    [Fact]
    public async Task GetAllFines_ReturnsOk()
    {
        Fine testfine = new Fine
        {
            Id = 1,
            Price = 100.00M,
            Payed = true,
            Description = "",
            Date = DateOnly.Parse("2001-01-1"),
            OwnerId = 1,
            CarId = 1
        };

        var fines = new List<Fine>(){testfine};

        _finerepoMock.Setup(r => r.GetFines())
            .ReturnsAsync(fines);

        var result = await _finecontroller.GetAllFines();

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task CreateFine_ReturnsBadRequestWhenNull()
    {
        _finerepoMock.Setup(r => r.InsertFine(null))
            .Returns((Task<FineDTO?>)null);
        
        var result = await _finecontroller.CreateFine(null);

        Assert.IsType<BadRequestResult>(result?.Result);
    }

    [Fact]
    public async Task ModifyFine_ReturnsOk()
    {
        Fine testfine = new Fine
        {
            Id = 1,
            Price = 100.00M,
            Payed = true,
            Description = "",
            Date = DateOnly.Parse("2001-01-1"),
            OwnerId = 1,
            CarId = 1
        };

        var fineDTO = new FineDTO
        {
            Id = testfine.Id,
            Price = testfine.Price,
            Payed = true,
            Description = testfine.Description,
            Date = testfine.Date,
            OwnerId = testfine.OwnerId,
            CarId = testfine.CarId
        };

        _finerepoMock.Setup(r => r.UpdateFine(1, testfine))
            .ReturnsAsync(fineDTO);

        var result = await _finecontroller.GetAllFines();

        Assert.IsType<OkObjectResult>(result);
    }
}