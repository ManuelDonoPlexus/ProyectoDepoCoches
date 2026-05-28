using CarDepo.API.DTOs.CarDriver;
using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarDepo.Infrastructure.Repositories;

public interface ICarDriverRepository
{
    Task<IEnumerable<CarDriver>> GetCarDrivers();
    Task<CarDriverDTO?> GetCarDriver(int CarDriverId);
    Task<CarDriverDTO?> InsertCarDriver(CarDriver? newCarDriver);
    Task<CarDriverDTO?> UpdateCarDriver(int CarDriverId, CarDriver newCarDriver);
    Task DeleteCarDriver(int CarDriverId);
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

    public async Task<CarDriverDTO?> GetCarDriver(int CarDriverId)
    {
        CarDriver? result = await _cardepocontext.CarDrivers
                .Include(cd => cd.Driver)
                .Include(cd => cd.Car)
                .AsNoTracking()
                .FirstOrDefaultAsync(cd => cd.Id == CarDriverId);

        if (result != null)
        {
            var dto = new CarDriverDTO()
            {
                Id = result.Id,
                DateDrive = result.DateDrive,
                CarCDId = result.CarCDId,
                DriverCDId = result.DriverCDId
            };
            return dto;
        }
        else { return null; }
    }

    public async Task<CarDriverDTO?> InsertCarDriver(CarDriver? newCarDriver)
    {
        if (newCarDriver != null)
        {
            EntityEntry<CarDriver> cardriver = _cardepocontext.CarDrivers.Add(newCarDriver);
            await _cardepocontext.SaveChangesAsync();
            CarDriver result = cardriver.Entity;

            var dto = new CarDriverDTO()
            {
                Id = result.Id,
                DateDrive = result.DateDrive,
                CarCDId = result.CarCDId,
                DriverCDId = result.DriverCDId
            };

            return dto;
        }
        else { return null; }
    }

    public async Task<CarDriverDTO?> UpdateCarDriver(int CarDriverId, CarDriver newCarDriver)
    {
        var result = await _cardepocontext.CarDrivers.FindAsync(CarDriverId);

        if (result != null)
        {
            result.DateDrive = newCarDriver.DateDrive;
            result.CarCDId = newCarDriver.CarCDId;
            result.DriverCDId = newCarDriver.DriverCDId;
            result.Car = newCarDriver.Car;
            result.Driver = newCarDriver.Driver;

            _cardepocontext.CarDrivers.Update(result);
            await _cardepocontext.SaveChangesAsync();

            var dto = new CarDriverDTO()
            {
                Id = result.Id,
                DateDrive = result.DateDrive,
                CarCDId = result.CarCDId,
                DriverCDId = result.DriverCDId
            };

            return dto;
        }
        else { return null; }
    }

    public async Task DeleteCarDriver(int CarDriverId)
    {
        var result = await _cardepocontext.CarDrivers.FindAsync(CarDriverId);
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