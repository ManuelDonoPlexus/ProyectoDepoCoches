using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarDepo.Infrastructure.Repositories;

public interface IDriverRepository
{
    Task<Driver> InsertDriver(Driver Driver);
    Task<Driver?> GetDriver(int DriverId);
    Task<IEnumerable<Driver>> GetDrivers();
    Task DeleteDriver(int DriverId);
    Task UpdateDriver(int DriverId, Driver Driver);
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

    public async Task<Driver> InsertDriver(Driver Driver)
    {
        EntityEntry<Driver> driver = _context.Drivers.Add(Driver);
        await _context.SaveChangesAsync();

        return driver.Entity;
    }

    public async Task UpdateDriver(int DriverId, Driver Driver)
    {
        if (DriverId == Driver.Id)
        {
            _context.Entry(Driver).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteDriver(int DriverId)
    {
        var Driver = await _context.Drivers.FindAsync(DriverId);
        if (Driver != null)
        {
            _context.Drivers.Remove(Driver);
            await _context.SaveChangesAsync();
        }
    }

    public bool IfDriverExists(int DriverId)
    {
        return _context.Drivers.Any(e => e.Id == DriverId);
    }
}