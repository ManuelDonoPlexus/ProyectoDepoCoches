using CarDepo.API.DTOs.Driver;
using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarDepo.Infrastructure.Repositories;

public interface IDriverRepository
{
    Task<IEnumerable<Driver>> GetDrivers();
    Task<DriverDTO?> GetDriver(int DriverId);
    Task<DriverDTO?> InsertDriver(Driver? newDriver);
    Task<DriverDTO?> UpdateDriver(int DriverId, Driver newDriver);
    Task DeleteDriver(int DriverId);
    bool IfDriverExists(int DriverId);
}

public class DriverRepository : IDriverRepository
{
    private readonly CarDepoContext _cardepocontext;

    public DriverRepository(CarDepoContext context)
    {
        _cardepocontext = context;
    }

    public async Task<IEnumerable<Driver>> GetDrivers()
    {
        return await _cardepocontext.Drivers
                .Include(d => d.Owner)
                .AsNoTracking()
                .ToListAsync();
    }

    public async Task<DriverDTO?> GetDriver(int DriverId)
    {
        Driver? result = await _cardepocontext.Drivers
                .Include(d => d.Owner)
                .AsNoTracking()
                .FirstOrDefaultAsync(cd => cd.Id == DriverId);

        if (result != null)
        {
            var dto = new DriverDTO()
            {
                Id = result.Id,
                Name = result.Name,
                Dni = result.Dni,
                EmailAddr = result.EmailAddr,
                PhoneNumber = result.PhoneNumber,
                OwnerId = result.OwnerId
            };
            
            return dto;
        }
        else { return null; }
    }

    public async Task<DriverDTO?> InsertDriver(Driver? newDriver)
    {
        if (newDriver != null)
        {
            EntityEntry<Driver> driver = _cardepocontext.Drivers.Add(newDriver);
            await _cardepocontext.SaveChangesAsync();
            Driver result = driver.Entity;

            var dto = new DriverDTO()
            {
                Id = result.Id,
                Name = result.Name,
                Dni = result.Dni,
                EmailAddr = result.EmailAddr,
                PhoneNumber = result.PhoneNumber,
                OwnerId = result.OwnerId
            };

            return dto;
        }
        else { return null; }
    }

    public async Task<DriverDTO?> UpdateDriver(int DriverId, Driver newDriver)
    {
        var result = await _cardepocontext.Drivers.FindAsync(DriverId);

        if (result != null)
        {
            result.Name = newDriver.Name;
            result.Dni = newDriver.Dni;
            result.EmailAddr = newDriver.EmailAddr;
            result.PhoneNumber = newDriver.PhoneNumber;
            result.OwnerId = newDriver.OwnerId;
            await _cardepocontext.SaveChangesAsync();

            var dto = new DriverDTO()
            {
                Id = result.Id,
                Name = result.Name,
                Dni = result.Dni,
                EmailAddr = result.EmailAddr,
                PhoneNumber = result.PhoneNumber,
                OwnerId = result.OwnerId
            };
            return dto;

        }
        else { return null; }
    }

    public async Task DeleteDriver(int DriverId)
    {
        var result = await _cardepocontext.Drivers.FindAsync(DriverId);
        if (result != null)
        {
            _cardepocontext.Drivers.Remove(result);
            await _cardepocontext.SaveChangesAsync();
        }
    }

    public bool IfDriverExists(int DriverId)
    {
        return _cardepocontext.Drivers.Any(e => e.Id == DriverId);
    }
}