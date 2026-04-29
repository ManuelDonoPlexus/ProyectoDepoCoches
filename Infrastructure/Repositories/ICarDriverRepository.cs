using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarDepo.Infrastructure.Repositories;

public interface ICarDriverRepository
{
    Task<CarDriver?> InsertCarDriver(CarDriver? newCarDriver);
    Task<CarDriver?> GetCarDriver(int CarDriverId);
    Task<IEnumerable<CarDriver>> GetCarDrivers();
    Task DeleteCarDriver(int CarDriverId);
    Task<CarDriver?> UpdateCarDriver(int CarDriverId, CarDriver newCarDriver);
    bool IfCarDriverExists(int CarDriverId);
}


public class CarDriverRepository : ICarDriverRepository
{
    private readonly CarDepoContext _context;

    public CarDriverRepository(CarDepoContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CarDriver>> GetCarDrivers()
    {
        return await _context.CarDrivers
                .Include(cd => cd.Driver)
                .Include(cd => cd.Car)
                .AsNoTracking()
                .ToListAsync();
    }

    public async Task<CarDriver?> GetCarDriver(int CarDriverId)
    {
        return await _context.CarDrivers
                .Include(cd => cd.Driver)
                .Include(cd => cd.Car)
                .AsNoTracking()
                .FirstOrDefaultAsync(cd => cd.Id == CarDriverId);
    }

    public async Task<CarDriver?> InsertCarDriver(CarDriver? newCarDriver)
    {
        if (newCarDriver != null)
        {
            EntityEntry<CarDriver> cardriver = _context.CarDrivers.Add(newCarDriver);
            await _context.SaveChangesAsync();
            return cardriver.Entity;
        } 
        else { return null; }
    }

    public async Task<CarDriver?> UpdateCarDriver(int CarDriverId, CarDriver newCarDriver)
    {
        var result = await _context.CarDrivers.FindAsync(CarDriverId);

        if (result != null)
        {
            result.DateDrive = newCarDriver.DateDrive;
            result.CarCDId = newCarDriver.CarCDId;
            result.DriverCDId = newCarDriver.DriverCDId;
            result.Car = newCarDriver.Car;
            result.Driver = newCarDriver.Driver;
            
            await _context.SaveChangesAsync();
            return result;
        }
        else { return null; }   
    }

    public async Task DeleteCarDriver(int CarDriverId)
    {
        var result = await GetCarDriver(CarDriverId);
        if (result != null)
        {
            _context.CarDrivers.Remove(result);
            await _context.SaveChangesAsync();
        }
    }

    public bool IfCarDriverExists(int CarDriverId)
    {
        return _context.CarDrivers.Any(e => e.Id == CarDriverId);
    }
}