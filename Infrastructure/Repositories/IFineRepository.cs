using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarDepo.Infrastructure.Repositories;

public interface IFineRepository
{
    Task<Fine?> InsertFine(Fine? fine);
    Task<Fine?> GetFine(int FineId);
    Task<IEnumerable<Fine>> GetFines();
    Task DeleteFine(int FineId);
    Task<Fine?> UpdateFine(int FineId, Fine newFine);
    bool IfFineExists(int FineId);
}

public class FineRepository : IFineRepository
{
    private readonly CarDepoContext _context;

    public FineRepository(CarDepoContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Fine>> GetFines()
    {
        return await _context.Fines
                .Include(f => f.Owner)
                .Include(f => f.Car)
                .AsNoTracking()
                .ToListAsync();
    }

    public async Task<Fine?> GetFine(int FineId)
    {
        return await _context.Fines
                .Include(f => f.Owner)
                .Include(f => f.Car)
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == FineId);
    }

    public async Task<Fine?> InsertFine(Fine? Fine)
    {
        if (Fine != null)
        {
            EntityEntry<Fine> fine = _context.Fines.Add(Fine);
            await _context.SaveChangesAsync();
            return fine.Entity;
        }
        else { return null; }
    }

    public async Task<Fine?> UpdateFine(int FineId, Fine newFine)
    {
        var result = await _context.Fines.FindAsync(newFine);

        if (result != null)
        {
            result.Date = newFine.Date;
            result.Description = newFine.Description;
            result.Price = newFine.Price;
            result.Payed = newFine.Payed;
            result.OwnerId = newFine.OwnerId;
            result.CarId = newFine.CarId;
            await _context.SaveChangesAsync();
            return result;
        }
        else { return null; }  
        
    }

    public async Task DeleteFine(int FineId)
    {
        var result = await GetFine(FineId);
        if (result != null)
        {
            _context.Fines.Remove(result);
            await _context.SaveChangesAsync();
        }
    }

    public bool IfFineExists(int FineId)
    {
        return _context.Fines.Any(e => e.Id == FineId);
    }
}