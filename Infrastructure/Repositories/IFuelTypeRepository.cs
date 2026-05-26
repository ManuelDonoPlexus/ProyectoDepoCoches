using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarDepo.Infrastructure.Repositories;

public interface IFuelTypeRepository
{
    Task<FuelType?> InsertFuelType(FuelType? newFuelType);
    Task<FuelType?> GetFuelType(int FuelTypeId);
    Task<IEnumerable<FuelType>> GetFuelTypes();
    Task DeleteFuelType(int FuelTypeId);
    Task<FuelType?> UpdateFuelType(int FuelTypeId, FuelType newFuelType);
    bool IfFuelTypeExists(int FuelTypeId);
}


public class FuelTypeRepository : IFuelTypeRepository
{
    private readonly CarDepoContext _cardepocontext;

    public FuelTypeRepository(CarDepoContext context)
    {
        _cardepocontext = context;
    }

    public async Task<IEnumerable<FuelType>> GetFuelTypes()
    {
        return await _cardepocontext.FuelTypes
                .AsNoTracking()
                .ToListAsync();
    }

    public async Task<FuelType?> GetFuelType(int FuelTypeId)
    {
        return await _cardepocontext.FuelTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == FuelTypeId);
    }

    public async Task<FuelType?> InsertFuelType(FuelType? newFuelType)
    {
        if (newFuelType != null)
        {
            EntityEntry<FuelType> fuelType = _cardepocontext.FuelTypes.Add(newFuelType);
            await _cardepocontext.SaveChangesAsync();
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
            await _cardepocontext.SaveChangesAsync();
            return result;
        }
        else { return null; }        
    }

    public async Task DeleteFuelType(int FuelTypeId)
    {
        var result = await GetFuelType(FuelTypeId);
        if (result != null)
        {
            _cardepocontext.FuelTypes.Remove(result);
            await _cardepocontext.SaveChangesAsync();
        }
    }

    public bool IfFuelTypeExists(int FuelTypeId)
    {
        return _cardepocontext.FuelTypes.Any(e => e.Id == FuelTypeId);
    }
}