using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarDepo.Infrastructure.Repositories;

public interface IOwnerRepository
{
    Task<Owner> InsertOwner(Owner Owner);
    Task<Owner?> GetOwner(int OwnerId);
    Task<IEnumerable<Owner>> GetOwners();
    Task DeleteOwner(int OwnerId);
    Task UpdateOwner(int OwnerId, Owner Owner);
    bool IfOwnerExists(int OwnerId);
}


public class OwnerRepository : IOwnerRepository
{
    private readonly CarDepoContext _context;

    public OwnerRepository(CarDepoContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Owner>> GetOwners()
    {
        return await _context.Owners
                .AsNoTracking()
                .ToListAsync();
    }

    public async Task<Owner?> GetOwner(int OwnerId)
    {
        return await _context.Owners
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == OwnerId);
    }

    public async Task<Owner> InsertOwner(Owner Owner)
    {
        EntityEntry<Owner> owner = _context.Owners.Add(Owner);
        await _context.SaveChangesAsync();

        return owner.Entity;
    }

    public async Task UpdateOwner(int OwnerId, Owner Owner)
    {
        if (OwnerId == Owner.Id)
        {
            _context.Entry(Owner).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteOwner(int OwnerId)
    {
        var Owner = await _context.Owners.FindAsync(OwnerId);
        if (Owner != null)
        {
            _context.Owners.Remove(Owner);
            await _context.SaveChangesAsync();
        }
    }

    public bool IfOwnerExists(int OwnerId)
    {
        return _context.Owners.Any(e => e.Id == OwnerId);
    }
}