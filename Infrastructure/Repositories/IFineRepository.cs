using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarDepo.Infrastructure.Repositories;

public interface IFineRepository
{
    Task<Fine> InsertFine(Fine fine);
    Task<Fine?> GetFine(int FineId);
    Task<IEnumerable<Fine>> GetFines();
    Task DeleteFine(int FineId);
    Task UpdateFine(int FineId, Fine Fine);
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

    public async Task<Fine> InsertFine(Fine Fine)
    {
        EntityEntry<Fine> fine = _context.Fines.Add(Fine);
        await _context.SaveChangesAsync();

        return fine.Entity;
    }

    public async Task UpdateFine(int FineId, Fine Fine)
    {
        if (FineId == Fine.Id)
        {
            _context.Entry(Fine).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteFine(int FineId)
    {
        var Fine = await _context.Fines.FindAsync(FineId);
        if (Fine != null)
        {
            _context.Fines.Remove(Fine);
            await _context.SaveChangesAsync();
        }
    }

    public bool IfFineExists(int FineId)
    {
        return _context.Fines.Any(e => e.Id == FineId);
    }
}