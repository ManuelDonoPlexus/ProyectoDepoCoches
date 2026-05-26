using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarDepo.Infrastructure.Repositories;

public interface IFineRepository
{
    Task<Fine?> InsertFine(Fine? newFine);
    Task<Fine?> GetFine(int FineId);
    Task<IEnumerable<Fine>> GetFines();
    Task DeleteFine(int FineId);
    Task<Fine?> UpdateFine(int FineId, Fine newFine);
    bool IfFineExists(int FineId);
}

public class FineRepository : IFineRepository
{
    private readonly CarDepoContext _cardepocontext;

    public FineRepository(CarDepoContext context)
    {
        _cardepocontext = context;
    }

    public async Task<IEnumerable<Fine>> GetFines()
    {
        return await _cardepocontext.Fines
                .Include(f => f.Owner)
                .Include(f => f.Car)
                .AsNoTracking()
                .ToListAsync();
    }

    public async Task<Fine?> GetFine(int FineId)
    {
        return await _cardepocontext.Fines
                .Include(f => f.Owner)
                .Include(f => f.Car)
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == FineId);
    }

    public async Task<Fine?> InsertFine(Fine? newFine)
    {
        if (newFine != null)
        {
            EntityEntry<Fine> fine = _cardepocontext.Fines.Add(newFine);
            await _cardepocontext.SaveChangesAsync();
            return fine.Entity;
        }
        else { return null; }
    }

    public async Task<Fine?> UpdateFine(int FineId, Fine newFine)
    {
        var result = await _cardepocontext.Fines.FindAsync(newFine);

        if (result != null)
        {
            result.Date = newFine.Date;
            result.Description = newFine.Description;
            result.Price = newFine.Price;
            result.Payed = newFine.Payed;
            result.OwnerId = newFine.OwnerId;
            result.CarId = newFine.CarId;
            await _cardepocontext.SaveChangesAsync();
            return result;
        }
        else { return null; }  
        
    }

    public async Task DeleteFine(int FineId)
    {
        var result = await GetFine(FineId);
        if (result != null)
        {
            _cardepocontext.Fines.Remove(result);
            await _cardepocontext.SaveChangesAsync();
        }
    }

    public bool IfFineExists(int FineId)
    {
        return _cardepocontext.Fines.Any(e => e.Id == FineId);
    }
}