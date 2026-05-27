using CarDepo.API.DTOs.Make;
using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarDepo.Infrastructure.Repositories;

public interface IMakeRepository
{
    Task<IEnumerable<Make>> GetMakes();
    Task<MakeGetDTO?> GetMake(int MakeId);
    Task<MakeInsertDTO?> InsertMake(Make? newMake);
    Task<MakeUpdateDTO?> UpdateMake(int MakeId, Make newMake);
    Task DeleteMake(int MakeId);
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

    public async Task<MakeGetDTO?> GetMake(int MakeId)
    {
        Make? result = await _cardepocontext.Makes
                .Include(m => m.FuelType)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == MakeId);

        if (result != null)
        {
            var dto = new MakeGetDTO()
            {
                Id = result.Id,
                Name = result.Name,
                HorsePower = result.HorsePower,
                Price = result.Price,
                FuelTypeId = result.FuelTypeId
            };

            return dto;
        }
        else { return null; }
    }

    public async Task<MakeInsertDTO?> InsertMake(Make? newMake)
    {
        if (newMake != null)
        {
            EntityEntry<Make> make = _cardepocontext.Makes.Add(newMake);
            await _cardepocontext.SaveChangesAsync();
            Make result = make.Entity;

            var dto = new MakeInsertDTO()
            {
                Id = result.Id,
                Name = result.Name,
                HorsePower = result.HorsePower,
                Price = result.Price,
                FuelTypeId = result.FuelTypeId
            };

            return dto;
        }
        else { return null; }
    }

    public async Task<MakeUpdateDTO?> UpdateMake(int MakeId, Make newMake)
    {
        var result = await _cardepocontext.Makes.FindAsync(MakeId);

        if (result != null)
        {
            result.Name = newMake.Name;
            result.Price = newMake.Price;
            result.FuelTypeId = newMake.FuelTypeId;
            await _cardepocontext.SaveChangesAsync();

            var dto = new MakeUpdateDTO()
            {
                Id = result.Id,
                Name = result.Name,
                HorsePower = result.HorsePower,
                Price = result.Price,
                FuelTypeId = result.FuelTypeId
            };

            return dto;
        }
        else { return null; }
    }

    public async Task DeleteMake(int MakeId)
    {
        var result = await _cardepocontext.Makes.FindAsync(MakeId);
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