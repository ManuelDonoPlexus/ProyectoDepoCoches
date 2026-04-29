using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarDepo.Infrastructure.Repositories;

public interface ICarDriverRepository
{
    Task<CarDriver?> InsertCarDriver(CarDriver? CarDriver);
    Task<CarDriver?> GetCarDriver(int CarDriverId);
    Task<IEnumerable<CarDriver>> GetCarDrivers();
    Task DeleteCarDriver(int CarDriverId);
    Task UpdateCarDriver(int CarDriverId, CarDriver CarDriver);
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

    public async Task<CarDriver?> InsertCarDriver(CarDriver? CarDriver)
    {
        if (CarDriver != null)
        {
            EntityEntry<CarDriver> cardriver = _context.CarDrivers.Add(CarDriver);
            await _context.SaveChangesAsync();
            return cardriver.Entity;
        } 
        else { return null; }
    }

    public async Task UpdateCarDriver(int CarDriverId, CarDriver CarDriver)
    {
        if (CarDriverId == CarDriver.Id)
        {
            _context.Entry(CarDriver).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteCarDriver(int CarDriverId)
    {
        var cardriver = await _context.CarDrivers.FindAsync(CarDriverId);
        if (cardriver != null)
        {
            _context.CarDrivers.Remove(cardriver);
            await _context.SaveChangesAsync();
        }
    }

    public bool IfCarDriverExists(int CarDriverId)
    {
        return _context.CarDrivers.Any(e => e.Id == CarDriverId);
    }
}