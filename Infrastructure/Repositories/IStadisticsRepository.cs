using System.Collections;
using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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
    private readonly CarDepoContext _cardepocontext;

    public StadisticsRepository(CarDepoContext cdc)
    {
        _cardepocontext = cdc;
    }

    public async Task<Decimal> GetAveragePrice()
    {
        IEnumerable<Make> makes = await _cardepocontext.Makes
                .Include(m => m.FuelType)
                .AsNoTracking()
                .ToListAsync();
        return makes.Average(m => m.Price);
    }

    public async Task<Double> GetCarKms()
    {
        IEnumerable<Car> cars = await _cardepocontext.Cars
            .Include(car => car.Color)
            .Include(car => car.Make)
            .Include(car => car.Owner)
            .AsNoTracking()
            .ToListAsync();
        return cars.Average(c => c.Kms);
    }

    public async Task<IEnumerable> GetCarColorCount()
    {
        IEnumerable<Car> cars = await _cardepocontext.Cars
            .Include(car => car.Color)
            .Include(car => car.Make)
            .Include(car => car.Owner)
            .AsNoTracking()
            .ToListAsync(); ;
        return cars.CountBy(c => c.ColorId);
    }

    public async Task<IEnumerable> GetCarDriverAssociatedCarCount()
    {
        IEnumerable<CarDriver> drivers = await _cardepocontext.CarDrivers
            .Include(cd => cd.Driver)
            .Include(cd => cd.Car)
            .AsNoTracking()
            .ToListAsync();
        return drivers.CountBy(d => d.CarCDId);
    }

    public async Task<IEnumerable> GetCarMakeCount()
    {
        IEnumerable<Car> cars = await _cardepocontext.Cars
            .Include(car => car.Color)
            .Include(car => car.Make)
            .Include(car => car.Owner)
            .AsNoTracking()
            .ToListAsync(); ;
        return cars.CountBy(c => c.MakeId);
    }

    public async Task<IEnumerable> GetCarOwnerCount()
    {
        IEnumerable<Car> cars = await _cardepocontext.Cars
            .Include(car => car.Color)
            .Include(car => car.Make)
            .Include(car => car.Owner)
            .AsNoTracking()
            .ToListAsync(); ;
        return cars.CountBy(c => c.OwnerId);
    }
}