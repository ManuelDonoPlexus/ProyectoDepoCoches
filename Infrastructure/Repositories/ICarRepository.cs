using CarDepo.API.DTOs.Car;
using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarDepo.Infrastructure.Repositories;

public interface ICarRepository
{
    Task<IEnumerable<Car>> GetCars();
    Task<CarGetDTO?> GetCar(int CarId);
    Task<CarInsertDTO?> InsertCar(Car? newCar);
    Task<CarUpdateDTO?> UpdateCar(int CarId, Car newCar);
    Task DeleteCar(int CarId);
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
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<CarGetDTO?> GetCar(int CarId)
    {
        Car? result = await _cardepocontext.Cars
            .Include(car => car.Color)
            .Include(car => car.Make)
            .Include(car => car.Owner)
            .AsNoTracking()
            .FirstOrDefaultAsync(car => car.Id == CarId);

        if (result != null)
        {
            var dto = new CarGetDTO()
            {
                Id = result.Id,
                License = result.License,
                Kms = result.Kms,
                ColorId = result.ColorId,
                OwnerId = result.OwnerId,
                MakeId = result.MakeId
            };
            return dto;
        }
        else { return null; }
    }

    public async Task<CarInsertDTO?> InsertCar(Car? newCar)
    {
        if (newCar != null)
        {
            EntityEntry<Car> car = _cardepocontext.Cars.Add(newCar);
            await _cardepocontext.SaveChangesAsync();
            Car result = car.Entity;

            var dto = new CarInsertDTO()
            {
                Id = result.Id,
                License = result.License,
                Kms = result.Kms,
                ColorId = result.ColorId,
                OwnerId = result.OwnerId,
                MakeId = result.MakeId
            };
            
            return dto;
        }
        else { return null; }
    }

    public async Task<CarUpdateDTO?> UpdateCar(int CarId, Car newCar)
    {
        var result = await _cardepocontext.Cars.FindAsync(CarId);

        if (result != null)
        {
            result.License = newCar.License;
            result.Kms = newCar.Kms;
            result.ColorId = newCar.ColorId;
            result.OwnerId = newCar.OwnerId;
            result.MakeId = newCar.MakeId;

            await _cardepocontext.SaveChangesAsync();

            var dto = new CarUpdateDTO()
            {
                Id = result.Id,
                License = result.License,
                Kms = result.Kms,
                ColorId = result.ColorId,
                OwnerId = result.OwnerId,
                MakeId = result.MakeId
            };

            return dto;
        }
        else { return null; }
    }

    public async Task DeleteCar(int CarId)
    {
        var result = await _cardepocontext.Cars.FindAsync(CarId);
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