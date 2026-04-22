using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarDepo.Infrastructure.Repositories;

public interface ICarRepository
{
    Task<Car> InsertCar(Car Car);
    Task<Car?> GetCar(int CarId);
    Task<IEnumerable<Car>> GetCars();
    Task DeleteCar(int CarId);
    Task UpdateCar(int CarId, Car Car);
    bool IfCarExists(int CarId);
}

public class CarRepository : ICarRepository
{
    private readonly CarDepoContext _context;

    public CarRepository(CarDepoContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Car>> GetCars()
    {
        return await _context.Cars
            .Include(car => car.Color)
            .Include(car => car.Make)
            .Include(car => car.Owner)
            .ToListAsync();
    }

    public async Task<Car?> GetCar(int CarId)
    {
        return await _context.Cars
            .Include(car => car.Color)
            .Include(car => car.Make)
            .Include(car => car.Owner)
            .FirstOrDefaultAsync(car => car.Id == CarId);
    }

    public async Task<Car> InsertCar(Car Car)
    {
        EntityEntry<Car> car = _context.Cars.Add(Car);
        await _context.SaveChangesAsync();

        return car.Entity;
    }

    public async Task UpdateCar(int CarId, Car Car)
    {
        if (CarId == Car.Id)
        {
            _context.Entry(Car).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteCar(int CarId)
    {
        var car = await _context.CarDrivers.FindAsync(CarId);
        if (car != null)
        {
            _context.CarDrivers.Remove(car);
            await _context.SaveChangesAsync();
        }
        
    }

    public bool IfCarExists(int CarId)
    {
        return _context.Cars.Any(e => e.Id == CarId);
    }
}