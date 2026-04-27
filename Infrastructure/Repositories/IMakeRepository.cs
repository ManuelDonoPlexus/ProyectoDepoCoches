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
    Task UpdateMake(int OwnerId, Make Owner);
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

    public async Task UpdateMake(int MakeId, Make Make)
    {
        if (MakeId == Make.Id)
        {
            _context.Entry(Make).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteMake(int MakeId)
    {
        var Make = await _context.Makes.FindAsync(MakeId);
        if (Make != null)
        {
            _context.Makes.Remove(Make);
            await _context.SaveChangesAsync();
        }
    }

    public bool IfMakeExists(int MakeId)
    {
        return _context.Makes.Any(e => e.Id == MakeId);
    }
}