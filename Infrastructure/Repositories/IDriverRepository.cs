using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarDepo.Infrastructure.Repositories;

public interface IDriverRepository
{
    Task<Driver?> InsertDriver(Driver? newDriver);
    Task<Driver?> GetDriver(int DriverId);
    Task<IEnumerable<Driver>> GetDrivers();
    Task DeleteDriver(int DriverId);
    Task<Driver?> UpdateDriver(int DriverId, Driver newDriver);
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

    public async Task<Driver?> GetDriver(int DriverId)
    {
        return await _cardepocontext.Drivers
                .Include(d => d.Owner)
                .AsNoTracking()
                .FirstOrDefaultAsync(cd => cd.Id == DriverId);
    }

    public async Task<Driver?> InsertDriver(Driver? newDriver)
    {
        if (newDriver != null)
        {
            EntityEntry<Driver> driver = _cardepocontext.Drivers.Add(newDriver);
            await _cardepocontext.SaveChangesAsync();
            return driver.Entity;
        }
        else { return null; }
    }

    public async Task<Driver?> UpdateDriver(int DriverId, Driver newDriver)
    {
        var result = await GetDriver(DriverId);

        if (result != null)
        {
            result.Name = newDriver.Name;
            result.Dni = newDriver.Dni;
            result.EmailAddr = newDriver.EmailAddr;
            result.PhoneNumber = newDriver.PhoneNumber;
            result.OwnerId = newDriver.OwnerId;
            await _cardepocontext.SaveChangesAsync();
            return result;
        }
        else { return null; }  
    }

    public async Task DeleteDriver(int DriverId)
    {
        var result = await GetDriver(DriverId);
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