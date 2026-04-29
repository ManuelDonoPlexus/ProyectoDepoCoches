using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
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

    // Implementa de la interfaz de repositorio el metodo InsertCar para añadir un coche a la base de datos
    public async Task<Car?> InsertCar(Car? newCar)
    {
        // Si el parametro no es nulo, se ejecuta se añade a la sesión
        // Si es nulo, se devuelve null
        if (newCar != null)
        {
            //Se añade a la sesión el coche pasado por parametro, se guardan los cambios realizados, y se devuelve la entidad resultante. 
            EntityEntry<Car> car = _context.Cars.Add(newCar);
            await _context.SaveChangesAsync();
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
            result.Color = newCar.Color;
            result.Owner = newCar.Owner;
            result.Make = newCar.Make;
            
            await _context.SaveChangesAsync();
            return result;
        }
        else { return null; }        
    }

    public async Task DeleteCar(int CarId)
    {
        var result = await GetCar(CarId);
        if (result != null)
        {
            _context.Cars.Remove(result);
            await _context.SaveChangesAsync();
        }

    }

    public bool IfCarExists(int CarId)
    {
        return _context.Cars.Any(e => e.Id == CarId);
    }
}