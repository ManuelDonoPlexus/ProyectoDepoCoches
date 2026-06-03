using System.Collections;
using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CarDepo.Infrastructure.Repositories;
// Capa de repositorio (encargada de obtener y manipular la información)

// Interfaz que señala los metodos iniciales
public interface IStadisticsRepository
{
    Task<Double> GetCarKms();
    Task<IEnumerable> GetCarColorCount();
    Task<IEnumerable> GetCarMakeCount();
    Task<IEnumerable> GetCarOwnerCount();
    Task<IEnumerable> GetCarDriverAssociatedCarCount();
    Task<Decimal> GetAveragePrice();
}

// Repositorio que implementa la interfaz de repositorio
public class StadisticsRepository : IStadisticsRepository
{
    private readonly CarDepoContext _cardepocontext; // contexto de la base de datos

    // Define las anteriores variables para su uso, inicializando la clase.
    public StadisticsRepository(CarDepoContext cdc)
    {
        _cardepocontext = cdc;
    }

    // Para obtener el precio promedio de las marcas de coches. 
    // Para ello, se obtiene una lista de todas las marcas, y se obtiene un Average segun su precio 
    public async Task<Decimal> GetAveragePrice()
    {
        IEnumerable<Make> makes = await _cardepocontext.Makes
                .Include(m => m.FuelType)
                .AsNoTracking()
                .ToListAsync();
        return makes.Average(m => m.Price);
    }

    // Para obtener el promedio de kilometros de los coches.
    // Para ello, se obtiene una lista de todos los coches, y se obtiene un Average segun sus kilometros 
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

    // Para obtener un recuento de coches segun su color.
    // Para ello, se obtiene una lista de todos los coches, y, apartir de esta, se crea otra lista que los filtra segun el Id de Color 
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

    // Para obtener un recuento de vehiculos segun la cantidad de conductores que los conducen
    // Para esto, obtenemos una lista de CarDrivers (la tabla intermedia para determinar que coche conduce cada conductor) y los filtramos por el Id del coche
    public async Task<IEnumerable> GetCarDriverAssociatedCarCount()
    {
        IEnumerable<CarDriver> drivers = await _cardepocontext.CarDrivers
            .Include(cd => cd.Driver)
            .Include(cd => cd.Car)
            .AsNoTracking()
            .ToListAsync();
        return drivers.CountBy(d => d.CarCDId);
    }

    // Para obtener un recuento de coches segun el modelo/marca que tienen
    // Para esto, obtenemos la lista de todos los coches, y la filtramos en otra segun el Id del modelo
    public async Task<IEnumerable> GetCarMakeCount()
    {
        IEnumerable<Car> cars = await _cardepocontext.Cars
            .Include(car => car.Color)
            .Include(car => car.Make)
            .Include(car => car.Owner)
            .AsNoTracking()
            .ToListAsync();
        return cars.CountBy(c => c.MakeId);
    }

    // Para obtener un recuento de coches que tiene cada propietario
    // Para conseguir esta lista, filtramos la lista de todos los coches segun el id de su propietario.
    public async Task<IEnumerable> GetCarOwnerCount()
    {
        IEnumerable<Car> cars = await _cardepocontext.Cars
            .Include(car => car.Color)
            .Include(car => car.Make)
            .Include(car => car.Owner)
            .AsNoTracking()
            .ToListAsync();
        return cars.CountBy(c => c.OwnerId);
    }
}