using CarDepo.API.DTOs.FuelType;
using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarDepo.Infrastructure.Repositories;

public interface IFuelTypeRepository
{
    Task<IEnumerable<FuelType>> GetFuelTypes();
    Task<FuelTypeDTO?> GetFuelType(int FuelTypeId);
    Task<FuelTypeDTO?> InsertFuelType(FuelType? newFuelType);
    Task<FuelTypeDTO?> UpdateFuelType(int FuelTypeId, FuelType newFuelType);
    Task DeleteFuelType(int FuelTypeId);
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

    public async Task<FuelTypeDTO?> GetFuelType(int FuelTypeId)
    {
        FuelType? result = await _cardepocontext.FuelTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == FuelTypeId);

        if (result != null)
        {
            var dto = new FuelTypeDTO()
            {
                Id = result.Id,
                Name = result.Name
            };

            return dto;
        }
        else { return null; }
    }

    public async Task<FuelTypeDTO?> InsertFuelType(FuelType? newFuelType)
    {
        if (newFuelType != null)
        {
            EntityEntry<FuelType> fuelType = _cardepocontext.FuelTypes.Add(newFuelType);
            await _cardepocontext.SaveChangesAsync();
            FuelType? result = fuelType.Entity;
            
            var dto = new FuelTypeDTO()
            {
                Id = result.Id,
                Name = result.Name
            };

            return dto;
        }
        else { return null; }
    }

    public async Task<FuelTypeDTO?> UpdateFuelType(int FuelTypeId, FuelType newFuelType)
    {
        var result = await _cardepocontext.FuelTypes.FindAsync(FuelTypeId);

        if (result != null)
        {
            result.Name = newFuelType.Name;

            _cardepocontext.FuelTypes.Update(result);
            await _cardepocontext.SaveChangesAsync();
            
            var dto = new FuelTypeDTO()
            {
                Id = result.Id,
                Name = result.Name
            };

            return dto;
        }
        else { return null; }        
    }

    public async Task DeleteFuelType(int FuelTypeId)
    {
        var result = await _cardepocontext.FuelTypes.FindAsync(FuelTypeId);
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