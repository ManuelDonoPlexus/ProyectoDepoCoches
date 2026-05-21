using System.Collections;
using CarDepo.API.Models;

namespace CarDepo.Infrastructure.Repositories;

public interface IStadisticsRepository
{
    Task<Double> GetCarKms();
    Task<IEnumerable> GetCarColorCount();
    Task<IEnumerable> GetCarMakeCount();
    Task<IEnumerable> GetCarOwnerCount();
    Task<IEnumerable> GetCarDriverAssociatedCarCount();
    Task<Decimal> GetAveragePrice();
}

public class StadisticsRepository : IStadisticsRepository
{
    private readonly ICarRepository _carrepo;
    private readonly IMakeRepository _makerepo;
    private readonly IDriverRepository _driverrepo;
    private readonly ICarDriverRepository _cardriverrepo;

    public StadisticsRepository(ICarRepository carrepo, IMakeRepository makerepo, IDriverRepository driverepo, ICarDriverRepository cardriverrepo)
    {
        _carrepo = carrepo;
        _makerepo = makerepo;
        _driverrepo = driverepo;
        _cardriverrepo = cardriverrepo;
    }

    public async Task<Decimal> GetAveragePrice()
    {
        IEnumerable<Make> makes = await _makerepo.GetMakes();
        return makes.Average(m => m.Price);
    }

    public async Task<Double> GetCarKms()
    {
        IEnumerable<Car> cars = await _carrepo.GetCars();
        return cars.Average(c => c.Kms);
    }

    public async Task<IEnumerable> GetCarColorCount()
    {
        IEnumerable<Car> cars = await _carrepo.GetCars();
        return cars.CountBy(c => c.ColorId);
    }

    public async Task<IEnumerable> GetCarDriverAssociatedCarCount()
    {
        IEnumerable<CarDriver> drivers = await _cardriverrepo.GetCarDrivers();
        return drivers.CountBy(d => d.CarCDId);
    }

    public async Task<IEnumerable> GetCarMakeCount()
    {
        IEnumerable<Car> cars = await _carrepo.GetCars();
        return cars.CountBy(c => c.MakeId);
    }

    public async Task<IEnumerable> GetCarOwnerCount()
    {
        IEnumerable<Car> cars = await _carrepo.GetCars();
        return cars.CountBy(c => c.OwnerId);
    }
}