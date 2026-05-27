using CarDepo.API.DTOs.Fine;
using CarDepo.API.Models;
using CarDepo.Infrastructure.Repositories;

namespace CarDepo.Application.Services;

public class FineService : IFineRepository
{
    private readonly IFineRepository _finerepo;

    public FineService(IFineRepository finerepo)
    {
        _finerepo = finerepo;
    }

    public async Task<IEnumerable<Fine>> GetFines()
    {
        return await _finerepo.GetFines();
    }

    public async Task<FineDTO?> GetFine(int FineId)
    {
        return await _finerepo.GetFine(FineId);
    }

    public async Task<FineDTO?> InsertFine(Fine? newFine)
    {
        return await _finerepo.InsertFine(newFine);
    }

    public async Task<FineDTO?> UpdateFine(int FineId, Fine newFine)
    {
        return await _finerepo.UpdateFine(FineId, newFine);
    }

    public async Task DeleteFine(int FineId)
    {
        await _finerepo.DeleteFine(FineId);
    }

    public bool IfFineExists(int FineId)
    {
        return _finerepo.IfFineExists(FineId);
    }
}