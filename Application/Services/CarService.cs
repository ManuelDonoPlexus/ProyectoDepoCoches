using CarDepo.API.DTOs.Car;
using CarDepo.API.Models;
using CarDepo.Infrastructure.Repositories;

namespace CarDepo.Application.Services;

public class CarService : ICarRepository
{
    private readonly ICarRepository _carrepo;

    public CarService(ICarRepository carrepo)
    {
        _carrepo = carrepo;
    } 

    public async Task<IEnumerable<Car>> GetCars()
    {
        return await _carrepo.GetCars();
    }

    public async Task<CarDTO?> GetCar(int CarId)
    {
        return await _carrepo.GetCar(CarId);
    }

    public async Task<CarDTO?> InsertCar(Car? newCar)
    {
        return await _carrepo.InsertCar(newCar);
    }

    public async Task<CarDTO?> UpdateCar(int CarId, Car newCar)
    {
        return await _carrepo.UpdateCar(CarId, newCar);
    }

    public async Task DeleteCar(int CarId)
    {
        await _carrepo.DeleteCar(CarId);
    }

    public bool IfCarExists(int CarId)
    {
        return _carrepo.IfCarExists(CarId);
    }
}