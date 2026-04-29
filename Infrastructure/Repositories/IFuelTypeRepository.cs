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
    Task<FuelType?> UpdateFuelType(int FuelTypeId, FuelType newFuelType);
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

    public async Task<FuelType?> UpdateFuelType(int FuelTypeId, FuelType newFuelType)
    {
        var result = await GetFuelType(FuelTypeId);

        if (result != null)
        {
            result.Name = newFuelType.Name;
            await _context.SaveChangesAsync();
            return result;
        }
        else { return null; }        
    }

    public async Task DeleteFuelType(int FuelTypeId)
    {
        var result = await GetFuelType(FuelTypeId);
        if (result != null)
        {
            _context.FuelTypes.Remove(result);
            await _context.SaveChangesAsync();
        }
    }

    public bool IfFuelTypeExists(int FuelTypeId)
    {
        return _context.FuelTypes.Any(e => e.Id == FuelTypeId);
    }
}