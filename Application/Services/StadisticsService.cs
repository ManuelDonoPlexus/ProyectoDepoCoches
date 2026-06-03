using System.Collections;
using CarDepo.Infrastructure.Repositories;

namespace CarDepo.Application.Services;
// Capa de servicioo (para comunicar los controladores y los repositorios)

// Clase del servicio, que implementa el repositorio por sus metodos.
public class StadisticsService : IStadisticsRepository
{
    private readonly IStadisticsRepository _stadisticsrepo;

    public StadisticsService(IStadisticsRepository stadisticsrepo)
    {
        _stadisticsrepo = stadisticsrepo;
    }

    public async Task<decimal> GetAveragePrice()
    {
        return await _stadisticsrepo.GetAveragePrice();
    }

    public async Task<IEnumerable> GetCarColorCount()
    {
        return await _stadisticsrepo.GetCarColorCount();
    }

    public async Task<IEnumerable> GetCarDriverAssociatedCarCount()
    {
        return await _stadisticsrepo.GetCarDriverAssociatedCarCount();
    }

    public async Task<double> GetCarKms()
    {
        return await _stadisticsrepo.GetCarKms();
    }

    public async Task<IEnumerable> GetCarMakeCount()
    {
        return await _stadisticsrepo.GetCarMakeCount();
    }

    public async Task<IEnumerable> GetCarOwnerCount()
    {
        return await _stadisticsrepo.GetCarOwnerCount();
    }
}