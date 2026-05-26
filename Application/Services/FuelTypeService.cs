using CarDepo.API.Models;
using CarDepo.Infrastructure.Repositories;

namespace CarDepo.Application.Services;

public class FuelTypeService : IFuelTypeRepository
{
    private readonly IFuelTypeRepository _fuelrepo;

    public FuelTypeService(IFuelTypeRepository fuelrepo)
    {
        _fuelrepo = fuelrepo;
    }

    public async Task<IEnumerable<FuelType>> GetFuelTypes()
    {
        return await _fuelrepo.GetFuelTypes();
    }

    public async Task<FuelType?> GetFuelType(int FuelTypeId)
    {
        return await _fuelrepo.GetFuelType(FuelTypeId);
    }

    public async Task<FuelType?> InsertFuelType(FuelType? newFuelType)
    {
        return await _fuelrepo.InsertFuelType(newFuelType);
    }

    public async Task<FuelType?> UpdateFuelType(int FuelTypeId, FuelType newFuelType)
    {
        return await _fuelrepo.UpdateFuelType(FuelTypeId, newFuelType);
    }

    public async Task DeleteFuelType(int FuelTypeId)
    {
        await _fuelrepo.DeleteFuelType(FuelTypeId);
    }

    public bool IfFuelTypeExists(int FuelTypeId)
    {
        return _fuelrepo.IfFuelTypeExists(FuelTypeId);
    }

}