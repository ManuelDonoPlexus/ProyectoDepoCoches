using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarDepo.Infrastructure.Repositories;

public interface IMakeRepository
{
    Task<Make?> InsertMake(Make? Owner);
    Task<Make?> GetMake(int OwnerId);
    Task<IEnumerable<Make>> GetMakes();
    Task DeleteMake(int OwnerId);
    Task<Make?> UpdateMake(int OwnerId, Make newOwner);
    bool IfMakeExists(int OwnerId);
}


public class MakeRepository : IMakeRepository
{
    private readonly CarDepoContext _context;

    public MakeRepository(CarDepoContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Make>> GetMakes()
    {
        return await _context.Makes
                .Include(m => m.FuelType)
                .AsNoTracking()
                .ToListAsync();
    }

    public async Task<Make?> GetMake(int MakeId)
    {
        return await _context.Makes
                .Include(m => m.FuelType)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == MakeId);
    }

    public async Task<Make?> InsertMake(Make? Make)
    {
        if (Make != null)
        {
            EntityEntry<Make> make = _context.Makes.Add(Make);
            await _context.SaveChangesAsync();
            return make.Entity;
        }
        else { return null; }
    }

    public async Task<Make?> UpdateMake(int MakeId, Make newMake)
    {
        var result = await GetMake(MakeId);

        if (result != null)
        {
            result.Name = newMake.Name;
            result.Price = newMake.Price;
            result.FuelTypeId = newMake.FuelTypeId;
            result.FuelType = newMake.FuelType;
            await _context.SaveChangesAsync();
            return result;
        }
        else { return null; }    
    }

    public async Task DeleteMake(int MakeId)
    {
        var result = await GetMake(MakeId);
        if (result != null)
        {
            _context.Makes.Remove(result);
            await _context.SaveChangesAsync();
        }
    }

    public bool IfMakeExists(int MakeId)
    {
        return _context.Makes.Any(e => e.Id == MakeId);
    }
}