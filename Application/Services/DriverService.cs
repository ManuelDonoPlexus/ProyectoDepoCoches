using CarDepo.API.DTOs.Driver;
using CarDepo.API.Models;
using CarDepo.Infrastructure.Repositories;

namespace CarDepo.Application.Services;

public class DriverService : IDriverRepository
{
    private readonly IDriverRepository _driverrepo;

    public DriverService(IDriverRepository driverrepo)
    {
        _driverrepo = driverrepo;
    }

    public async Task<IEnumerable<Driver>> GetDrivers()
    {
        return await _driverrepo.GetDrivers();
    }


    public async Task<DriverDTO?> GetDriver(int DriverId)
    {
        return await _driverrepo.GetDriver(DriverId);
    }

    public async Task<DriverDTO?> InsertDriver(Driver? newDriver)
    {
        return await _driverrepo.InsertDriver(newDriver);
    }

    public async Task<DriverDTO?> UpdateDriver(int DriverId, Driver newDriver)
    {
        return await _driverrepo.UpdateDriver(DriverId, newDriver);
    }

    public async Task DeleteDriver(int DriverId)
    {
        await _driverrepo.DeleteDriver(DriverId);
    }
    
    public bool IfDriverExists(int DriverId)
    {
        return _driverrepo.IfDriverExists(DriverId);
    }
}