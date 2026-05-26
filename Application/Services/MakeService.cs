using CarDepo.API.Models;
using CarDepo.Infrastructure.Repositories;

namespace CarDepo.Application.Services;

public class MakeService : IMakeRepository
{
    private readonly IMakeRepository _makerepo;

    public MakeService(IMakeRepository makerepo)
    {
        _makerepo = makerepo;
    }

    public async Task<IEnumerable<Make>> GetMakes()
    {
        return await _makerepo.GetMakes();
    }

    public async Task<Make?> GetMake(int MakeId)
    {
        return await _makerepo.GetMake(MakeId);
    }

    public async Task<Make?> InsertMake(Make? newMake)
    {
        return await _makerepo.InsertMake(newMake);
    }

    public async Task<Make?> UpdateMake(int MakeId, Make newMake)
    {
        return await _makerepo.UpdateMake(MakeId, newMake);
    }

    public async Task DeleteMake(int MakeId)
    {
        await _makerepo.DeleteMake(MakeId);
    }

    public bool IfMakeExists(int MakeId)
    {
        return _makerepo.IfMakeExists(MakeId);
    }
}