using CarDepo.API.DTOs.Owner;
using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarDepo.Infrastructure.Repositories;

public interface IOwnerRepository
{
    Task<IEnumerable<Owner>> GetOwners();
    Task<OwnerDTO?> GetOwner(int OwnerId);
    Task<OwnerDTO?> InsertOwner(Owner? newOwner);
    Task<OwnerDTO?> UpdateOwner(int OwnerId, Owner Owner);
    Task DeleteOwner(int OwnerId);
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

    public async Task<OwnerDTO?> GetOwner(int OwnerId)
    {
        Owner? result = await _cardepocontext.Owners
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == OwnerId);

        if (result != null)
        {
            var dto = new OwnerDTO()
            {
                Id = result.Id,
                Name = result.Name,
                Nif = result.Nif,
                PhoneNumber = result.PhoneNumber,
                DateEntry = result.DateEntry,
                EmailAddr = result.EmailAddr
            };

            return dto;
        }
        else { return null; }
    }

    public async Task<OwnerDTO?> InsertOwner(Owner? newOwner)
    {
        if (newOwner != null)
        {
            EntityEntry<Owner> owner = _cardepocontext.Owners.Add(newOwner);
            await _cardepocontext.SaveChangesAsync();
            Owner result = owner.Entity;

            var dto = new OwnerDTO()
            {
                Id = result.Id,
                Name = result.Name,
                Nif = result.Nif,
                PhoneNumber = result.PhoneNumber,
                DateEntry = result.DateEntry,
                EmailAddr = result.EmailAddr
            };

            return dto;
        }
        else { return null; }
    }

    public async Task<OwnerDTO?> UpdateOwner(int OwnerId, Owner newOwner)
    {
        var result = await _cardepocontext.Owners.FindAsync(OwnerId);

        if (result != null)
        {
            result.Name = newOwner.Name;
            result.Nif = newOwner.Nif;
            result.PhoneNumber = newOwner.PhoneNumber;
            result.DateEntry = newOwner.DateEntry;
            result.EmailAddr = newOwner.EmailAddr;
            
            _cardepocontext.Owners.Update(result);
            await _cardepocontext.SaveChangesAsync();

            var dto = new OwnerDTO()
            {
                Id = result.Id,
                Name = result.Name,
                Nif = result.Nif,
                PhoneNumber = result.PhoneNumber,
                DateEntry = result.DateEntry,
                EmailAddr = result.EmailAddr
            };

            return dto;
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