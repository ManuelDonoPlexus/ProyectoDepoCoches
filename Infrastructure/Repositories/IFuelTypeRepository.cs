using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarDepo.Infrastructure.Repositories;

public interface IFuelTypeRepository
{
    Task<FuelType?> InsertFuelType(FuelType? FuelType);
    Task<FuelType?> GetFuelType(int FuelTypeId);
    Task<IEnumerable<FuelType>> GetFuelTypes();
    Task DeleteFuelType(int FuelTypeId);
    Task UpdateFuelType(int FuelTypeId, FuelType FuelType);
    bool IfFuelTypeExists(int FuelTypeId);
}


public class FuelTypeRepository : IFuelTypeRepository
{
    private readonly CarDepoContext _context;

    public FuelTypeRepository(CarDepoContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<FuelType>> GetFuelTypes()
    {
        return await _context.FuelTypes
                .AsNoTracking()
                .ToListAsync();
    }

    public async Task<FuelType?> GetFuelType(int FuelTypeId)
    {
        return await _context.FuelTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == FuelTypeId);
    }

    public async Task<FuelType?> InsertFuelType(FuelType? FuelType)
    {
        if (FuelType != null)
        {
            EntityEntry<FuelType> fuelType = _context.FuelTypes.Add(FuelType);
            await _context.SaveChangesAsync();
            return fuelType.Entity;
        }
        else { return null; }
    }

    public async Task UpdateFuelType(int FuelTypeId, FuelType FuelType)
    {
        if (FuelTypeId == FuelType.Id)
        {
            _context.Entry(FuelType).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteFuelType(int FuelTypeId)
    {
        var FuelType = await _context.FuelTypes.FindAsync(FuelTypeId);
        if (FuelType != null)
        {
            _context.FuelTypes.Remove(FuelType);
            await _context.SaveChangesAsync();
        }
    }

    public bool IfFuelTypeExists(int FuelTypeId)
    {
        return _context.FuelTypes.Any(e => e.Id == FuelTypeId);
    }
}