using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarDepo.Infrastructure.Repositories;

public interface ICarDriverRepository
{
    Task<CarDriver?> GetCarDriver(int CarDriverId);
    Task<IEnumerable<CarDriver>> GetCarDrivers();
    Task<CarDriver?> InsertCarDriver(CarDriver? newCarDriver);
    Task DeleteCarDriver(int CarDriverId);
    Task<CarDriver?> UpdateCarDriver(int CarDriverId, CarDriver newCarDriver);
    bool IfCarDriverExists(int CarDriverId);
}


public class CarDriverRepository : ICarDriverRepository
{
    private readonly CarDepoContext _cardepocontext;

    public CarDriverRepository(CarDepoContext context)
    {
        _cardepocontext = context;
    }

    public async Task<IEnumerable<CarDriver>> GetCarDrivers()
    {
        return await _cardepocontext.CarDrivers
                .Include(cd => cd.Driver)
                .Include(cd => cd.Car)
                .AsNoTracking()
                .ToListAsync();
    }

    public async Task<CarDriver?> GetCarDriver(int CarDriverId)
    {
        return await _cardepocontext.CarDrivers
                .Include(cd => cd.Driver)
                .Include(cd => cd.Car)
                .AsNoTracking()
                .FirstOrDefaultAsync(cd => cd.Id == CarDriverId);
    }

    public async Task<CarDriver?> InsertCarDriver(CarDriver? newCarDriver)
    {
        if (newCarDriver != null)
        {
            EntityEntry<CarDriver> cardriver = _cardepocontext.CarDrivers.Add(newCarDriver);
            await _cardepocontext.SaveChangesAsync();
            return cardriver.Entity;
        } 
        else { return null; }
    }

    public async Task<CarDriver?> UpdateCarDriver(int CarDriverId, CarDriver newCarDriver)
    {
        var result = await _cardepocontext.CarDrivers.FindAsync(CarDriverId);

        if (result != null)
        {
            result.DateDrive = newCarDriver.DateDrive;
            result.CarCDId = newCarDriver.CarCDId;
            result.DriverCDId = newCarDriver.DriverCDId;
            
            await _cardepocontext.SaveChangesAsync();
            return result;
        }
        else { return null; }   
    }

    public async Task DeleteCarDriver(int CarDriverId)
    {
        var result = await GetCarDriver(CarDriverId);
        if (result != null)
        {
            _cardepocontext.CarDrivers.Remove(result);
            await _cardepocontext.SaveChangesAsync();
        }
    }

    public bool IfCarDriverExists(int CarDriverId)
    {
        return _cardepocontext.CarDrivers.Any(e => e.Id == CarDriverId);
    }
}