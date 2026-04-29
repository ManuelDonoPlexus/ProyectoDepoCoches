using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarDepo.Infrastructure.Repositories;

public interface IDriverRepository
{
    Task<Driver?> InsertDriver(Driver? Driver);
    Task<Driver?> GetDriver(int DriverId);
    Task<IEnumerable<Driver>> GetDrivers();
    Task DeleteDriver(int DriverId);
    Task<Driver?> UpdateDriver(int DriverId, Driver newDriver);
    bool IfDriverExists(int DriverId);
}

public class DriverRepository : IDriverRepository
{
    private readonly CarDepoContext _context;

    public DriverRepository(CarDepoContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Driver>> GetDrivers()
    {
        return await _context.Drivers
                .Include(d => d.Owner)
                .AsNoTracking()
                .ToListAsync();
    }

    public async Task<Driver?> GetDriver(int DriverId)
    {
        return await _context.Drivers
                .Include(d => d.Owner)
                .AsNoTracking()
                .FirstOrDefaultAsync(cd => cd.Id == DriverId);
    }

    public async Task<Driver?> InsertDriver(Driver? Driver)
    {
        if (Driver != null)
        {
            EntityEntry<Driver> driver = _context.Drivers.Add(Driver);
            await _context.SaveChangesAsync();
            return driver.Entity;
        }
        else { return null; }
    }

    public async Task<Driver?> UpdateDriver(int DriverId, Driver newDriver)
    {
        var result = await _context.Drivers.FindAsync(newDriver);

        if (result != null)
        {
            result.Name = newDriver.Name;
            result.Dni = newDriver.Dni;
            result.EmailAddr = newDriver.EmailAddr;
            result.PhoneNumber = newDriver.PhoneNumber;
            result.OwnerId = newDriver.OwnerId;
            result.Owner = newDriver.Owner;
            await _context.SaveChangesAsync();
            return result;
        }
        else { return null; }  
    }

    public async Task DeleteDriver(int DriverId)
    {
        var result = await GetDriver(DriverId);
        if (result != null)
        {
            _context.Drivers.Remove(result);
            await _context.SaveChangesAsync();
        }
    }

    public bool IfDriverExists(int DriverId)
    {
        return _context.Drivers.Any(e => e.Id == DriverId);
    }
}