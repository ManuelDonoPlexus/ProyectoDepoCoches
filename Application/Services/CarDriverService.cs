using CarDepo.API.DTOs.CarDriver;
using CarDepo.API.Models;
using CarDepo.Infrastructure.Repositories;

namespace CarDepo.Application.Services;

public class CarDriverService : ICarDriverRepository
{
    private readonly ICarDriverRepository _cardriverrepo;

    public CarDriverService(ICarDriverRepository cardriverrepo)
    {
        _cardriverrepo = cardriverrepo;
    }

    public async Task<IEnumerable<CarDriver>> GetCarDrivers()
    {
        return await _cardriverrepo.GetCarDrivers();
    }

    public async Task<CarDriverDTO?> GetCarDriver(int CarDriverId)
    {
        return await _cardriverrepo.GetCarDriver(CarDriverId);
    }

    public async Task<CarDriverDTO?> InsertCarDriver(CarDriver? newCarDriver)
    {
        return await _cardriverrepo.InsertCarDriver(newCarDriver);
    }

    public async Task<CarDriverDTO?> UpdateCarDriver(int CarDriverId, CarDriver newCarDriver)
    {
        return await _cardriverrepo.UpdateCarDriver(CarDriverId, newCarDriver);
    }

    public async Task DeleteCarDriver(int CarDriverId)
    {
        await _cardriverrepo.DeleteCarDriver(CarDriverId);
    }

    public bool IfCarDriverExists(int CarDriverId)
    {
        return IfCarDriverExists(CarDriverId);
    }
}