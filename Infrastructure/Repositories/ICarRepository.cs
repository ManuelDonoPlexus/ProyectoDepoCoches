using CarDepo.API.DTOs.Car;
using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarDepo.Infrastructure.Repositories;
// Capa de repositorio (encargada de obtener y manipular la información)

// Interfaz que señala los metodos iniciales
public interface ICarRepository
{
    Task<IEnumerable<Car>> GetCars();
    Task<CarDTO?> GetCar(int CarId);
    Task<CarDTO?> InsertCar(Car? newCar);
    Task<CarDTO?> UpdateCar(int CarId, Car newCar);
    Task DeleteCar(int CarId);
    bool IfCarExists(int CarId);
}

// Repositorio que implementa la interfaz de repositorio
public class CarRepository : ICarRepository
{
    private readonly CarDepoContext _cardepocontext; // contexto de la base de datos

    // Define las anteriores variables para su uso, inicializando la clase.
    public CarRepository(CarDepoContext context)
    {
        _cardepocontext = context;
    }

    // Obtiene todas las entidades de una tabla correspondiente con la entidad asocidada al repositorio
    public async Task<IEnumerable<Car>> GetCars()
    {
        return await _cardepocontext.Cars
            .Include(car => car.Color)
            .Include(car => car.Make)
            .Include(car => car.Owner)
            .AsNoTracking()
            .ToListAsync();
    }

    // Busca a la entidad que coincidan con el Id recibido como parametro en la Base de Datos
    // Si se encuentra, es mapeado a un DTO y devuelto como resultado
    // Si no se encuentra, se devuelve nulo 
    public async Task<CarDTO?> GetCar(int CarId)
    {
        Car? result = await _cardepocontext.Cars
            .Include(car => car.Color)
            .Include(car => car.Make)
            .Include(car => car.Owner)
            .AsNoTracking()
            .FirstOrDefaultAsync(car => car.Id == CarId);

        if (result != null)
        {
            var dto = new CarDTO()
            {
                Id = result.Id,
                License = result.License,
                Kms = result.Kms,
                ColorId = result.ColorId,
                OwnerId = result.OwnerId,
                MakeId = result.MakeId
            };
            return dto;
        }
        else { return null; }
    }

    // Para insertar una nueva entidad. 
    // Si no es nulo el paranetro, se añade la nueva entidad a la base de datos, se mapea a un DTO y se devuelve como resultado
    // Si el nulo, se devuelve nulo 
    public async Task<CarDTO?> InsertCar(Car? newCar)
    {
        if (newCar != null)
        {
            EntityEntry<Car> car = _cardepocontext.Cars.Add(newCar);
            await _cardepocontext.SaveChangesAsync();
            Car result = car.Entity;

            var dto = new CarDTO()
            {
                Id = result.Id,
                License = result.License,
                Kms = result.Kms,
                ColorId = result.ColorId,
                OwnerId = result.OwnerId,
                MakeId = result.MakeId
            };
            
            return dto;
        }
        else { return null; }
    }

    // Para actualizar una entidad. Para esto, se utiliza como parametro el Id de la entidad a manipular y el nuevo conductor
    // Si se encuentra la entidad con el Id, el se cambian los datos de la entidad por los de la nueva, se actualiza la entidad y se guardan los resultados, y se devuelve un DTO como resultado
    // Si no se encuentra, se devuelve nulo
    public async Task<CarDTO?> UpdateCar(int CarId, Car newCar)
    {
        var result = await _cardepocontext.Cars.FindAsync(CarId);

        if (result != null)
        {
            result.License = newCar.License;
            result.Kms = newCar.Kms;
            result.ColorId = newCar.ColorId;
            result.OwnerId = newCar.OwnerId;
            result.MakeId = newCar.MakeId;
            result.Color = newCar.Color;
            result.Make = newCar.Make;
            result.Owner = newCar.Owner;
            
            _cardepocontext.Cars.Update(result);
            await _cardepocontext.SaveChangesAsync();

            var dto = new CarDTO()
            {
                Id = result.Id,
                License = result.License,
                Kms = result.Kms,
                ColorId = result.ColorId,
                OwnerId = result.OwnerId,
                MakeId = result.MakeId
            };

            return dto;
        }
        else { return null; }
    }

    // Para borrar una entidad de la base de datos. Recibe el Id de la entidad a borrar.
    // Utilizando este Id, busca a la entidad. Si la encuentra, la borra de la base de datos.
    public async Task DeleteCar(int CarId)
    {
        var result = await _cardepocontext.Cars.FindAsync(CarId);
        if (result != null)
        {
            _cardepocontext.Cars.Remove(result);
            await _cardepocontext.SaveChangesAsync();
        }

    }

    // Para comprobar si una entidad existe o no. 
    // Dependencia de su existencia, devuelve un true o un false
    public bool IfCarExists(int CarId)
    {
        return _cardepocontext.Cars.Any(e => e.Id == CarId);
    }
}