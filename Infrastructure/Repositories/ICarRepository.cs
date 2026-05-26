using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarDepo.Infrastructure.Repositories;

public interface ICarRepository
{
    Task<Car?> InsertCar(Car? newCar);
    Task<Car?> GetCar(int CarId);
    Task<IEnumerable<Car>> GetCars();
    Task DeleteCar(int CarId);
    Task<Car?> UpdateCar(int CarId, Car newCar);
    bool IfCarExists(int CarId);
}

public class CarRepository : ICarRepository
{
    private readonly CarDepoContext _cardepocontext;

    public CarRepository(CarDepoContext context)
    {
        _cardepocontext = context;
    }

    public async Task<IEnumerable<Car>> GetCars()
    {
        return await _cardepocontext.Cars
            .Include(car => car.Color)
            .Include(car => car.Make)
            .Include(car => car.Owner)
            .ToListAsync();
    }

    public async Task<Car?> GetCar(int CarId)
    {
        return await _cardepocontext.Cars
            .Include(car => car.Color)
            .Include(car => car.Make)
            .Include(car => car.Owner)
            .FirstOrDefaultAsync(car => car.Id == CarId);
    }

    public async Task<Car?> InsertCar(Car? newCar)
    {
        if (newCar != null)
        {
            EntityEntry<Car> car = _cardepocontext.Cars.Add(newCar);
            await _cardepocontext.SaveChangesAsync();
            return car?.Entity;
        } 
        else { return null; }
    }

    public async Task<Car?> UpdateCar(int CarId, Car newCar)
    {
        var result = await GetCar(CarId);

        if (result != null)
        {
            result.License = newCar.License;
            result.Kms = newCar.Kms;
            result.ColorId = newCar.ColorId;
            result.OwnerId = newCar.OwnerId;
            result.MakeId = newCar.MakeId;
            
            await _cardepocontext.SaveChangesAsync();
            return result;
        }
        else { return null; }        
    }

    public async Task DeleteCar(int CarId)
    {
        var result = await GetCar(CarId);
        if (result != null)
        {
            _cardepocontext.Cars.Remove(result);
            await _cardepocontext.SaveChangesAsync();
        }

    }

    public bool IfCarExists(int CarId)
    {
        return _cardepocontext.Cars.Any(e => e.Id == CarId);
    }
}