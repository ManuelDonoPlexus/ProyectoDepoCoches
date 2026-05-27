using CarDepo.API.Models;
using CarDepo.Infrastructure.Repositories;

namespace CarDepo.Application.Services;

public class OwnerService : IOwnerRepository
{
    private readonly IOwnerRepository _ownerrepo;

    public OwnerService(IOwnerRepository ownerrepo)
    {
        _ownerrepo = ownerrepo;
    }

    public async Task<IEnumerable<Owner>> GetOwners()
    {
        return await _ownerrepo.GetOwners();
    }

    public async Task<Owner?> GetOwner(int OwnerId)
    {
        return await _ownerrepo.GetOwner(OwnerId);
    }

    public async Task<Owner?> InsertOwner(Owner? newOwner)
    {
        return await _ownerrepo.InsertOwner(newOwner);
    }

    public async Task<Owner?> UpdateOwner(int OwnerId, Owner newOwner)
    {
        return await _ownerrepo.UpdateOwner(OwnerId, newOwner);
    }

    public async Task DeleteOwner(int OwnerId)
    {
        await _ownerrepo.DeleteOwner(OwnerId);
    }
    
    public bool IfOwnerExists(int OwnerId)
    {
        return _ownerrepo.IfOwnerExists(OwnerId);
    }

}