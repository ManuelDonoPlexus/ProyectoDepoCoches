using CarDepo.API.DTOs.Make;
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

    public async Task<MakeGetDTO?> GetMake(int MakeId)
    {
        return await _makerepo.GetMake(MakeId);
    }

    public async Task<MakeInsertDTO?> InsertMake(Make? newMake)
    {
        return await _makerepo.InsertMake(newMake);
    }

    public async Task<MakeUpdateDTO?> UpdateMake(int MakeId, Make newMake)
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