using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarDepo.Infrastructure.Repositories;

public interface IMakeRepository
{
    Task<Make?> InsertMake(Make? newMake);
    Task<Make?> GetMake(int MakeId);
    Task<IEnumerable<Make>> GetMakes();
    Task DeleteMake(int MakeId);
    Task<Make?> UpdateMake(int MakeId, Make newMake);
    bool IfMakeExists(int MakeId);
}


public class MakeRepository : IMakeRepository
{
    private readonly CarDepoContext _cardepocontext;

    public MakeRepository(CarDepoContext context)
    {
        _cardepocontext = context;
    }

    public async Task<IEnumerable<Make>> GetMakes()
    {
        return await _cardepocontext.Makes
                .Include(m => m.FuelType)
                .AsNoTracking()
                .ToListAsync();
    }

    public async Task<Make?> GetMake(int MakeId)
    {
        return await _cardepocontext.Makes
                .Include(m => m.FuelType)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == MakeId);
    }

    public async Task<Make?> InsertMake(Make? newMake)
    {
        if (newMake != null)
        {
            EntityEntry<Make> make = _cardepocontext.Makes.Add(newMake);
            await _cardepocontext.SaveChangesAsync();
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
            await _cardepocontext.SaveChangesAsync();
            return result;
        }
        else { return null; }    
    }

    public async Task DeleteMake(int MakeId)
    {
        var result = await GetMake(MakeId);
        if (result != null)
        {
            _cardepocontext.Makes.Remove(result);
            await _cardepocontext.SaveChangesAsync();
        }
    }

    public bool IfMakeExists(int MakeId)
    {
        return _cardepocontext.Makes.Any(e => e.Id == MakeId);
    }
}