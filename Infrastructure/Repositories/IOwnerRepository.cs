using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarDepo.Infrastructure.Repositories;

public interface IOwnerRepository
{
    Task<Owner?> InsertOwner(Owner? newOwner);
    Task<Owner?> GetOwner(int OwnerId);
    Task<IEnumerable<Owner>> GetOwners();
    Task DeleteOwner(int OwnerId);
    Task<Owner?> UpdateOwner(int OwnerId, Owner Owner);
    bool IfOwnerExists(int OwnerId);
}


public class OwnerRepository : IOwnerRepository
{
    private readonly CarDepoContext _cardepocontext;

    public OwnerRepository(CarDepoContext context)
    {
        _cardepocontext = context;
    }

    public async Task<IEnumerable<Owner>> GetOwners()
    {
        return await _cardepocontext.Owners
                .AsNoTracking()
                .ToListAsync();
    }

    public async Task<Owner?> GetOwner(int OwnerId)
    {
        return await _cardepocontext.Owners
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == OwnerId);
    }

    public async Task<Owner?> InsertOwner(Owner? newOwner)
    {
        if (newOwner != null)
        {
            EntityEntry<Owner> owner = _cardepocontext.Owners.Add(newOwner);
            await _cardepocontext.SaveChangesAsync();
            return owner.Entity;
        }
        else { return null; }
    }

    public async Task<Owner?> UpdateOwner(int OwnerId, Owner newOwner)
    {
        var result = await GetOwner(OwnerId);

        if (result != null)
        {
            result.Name = newOwner.Name;
            result.Nif = newOwner.Nif;
            result.PhoneNumber = newOwner.PhoneNumber;
            result.DateEntry = newOwner.DateEntry;
            result.EmailAddr = newOwner.EmailAddr;
            await _cardepocontext.SaveChangesAsync();
            return result;
        }
        else { return null; }
    }

    public async Task DeleteOwner(int OwnerId)
    {
        var result = await _cardepocontext.Owners.FindAsync(OwnerId);
        if (result != null)
        {
            _cardepocontext.Owners.Remove(result);
            await _cardepocontext.SaveChangesAsync();
        }
    }

    public bool IfOwnerExists(int OwnerId)
    {
        return _cardepocontext.Owners.Any(e => e.Id == OwnerId);
    }
}