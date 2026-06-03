using CarDepo.API.DTOs.CarDriver;
using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarDepo.Infrastructure.Repositories;
// Capa de repositorio (encargada de obtener y manipular la información)

// Interfaz que señala los metodos iniciales
public interface ICarDriverRepository
{
    Task<IEnumerable<CarDriver>> GetCarDrivers();
    Task<CarDriverDTO?> GetCarDriver(int CarDriverId);
    Task<CarDriverDTO?> InsertCarDriver(CarDriver? newCarDriver);
    Task<CarDriverDTO?> UpdateCarDriver(int CarDriverId, CarDriver newCarDriver);
    Task DeleteCarDriver(int CarDriverId);
    bool IfCarDriverExists(int CarDriverId);
}

// Repositorio que implementa la interfaz de repositorio
public class CarDriverRepository : ICarDriverRepository
{
    private readonly CarDepoContext _cardepocontext; // contexto de la base de datos

    // Define las anteriores variables para su uso, inicializando la clase.
    public CarDriverRepository(CarDepoContext context)
    {
        _cardepocontext = context;
    }

    // Obtiene todas las entidades de una tabla correspondiente con la entidad asocidada al repositorio
    public async Task<IEnumerable<CarDriver>> GetCarDrivers()
    {
        return await _cardepocontext.CarDrivers
                .Include(cd => cd.Driver)
                .Include(cd => cd.Car)
                .AsNoTracking()
                .ToListAsync();
    }

    // Busca a la entidad que coincidan con el Id recibido como parametro en la Base de Datos
    // Si se encuentra, es mapeado a un DTO y devuelto como resultado
    // Si no se encuentra, se devuelve nulo 
    public async Task<CarDriverDTO?> GetCarDriver(int CarDriverId)
    {
        CarDriver? result = await _cardepocontext.CarDrivers
                .Include(cd => cd.Driver)
                .Include(cd => cd.Car)
                .AsNoTracking()
                .FirstOrDefaultAsync(cd => cd.Id == CarDriverId);

        if (result != null)
        {
            var dto = new CarDriverDTO()
            {
                Id = result.Id,
                DateDrive = result.DateDrive,
                CarCDId = result.CarCDId,
                DriverCDId = result.DriverCDId
            };
            return dto;
        }
        else { return null; }
    }

    // Para insertar una nueva entidad. 
    // Si no es nulo el paranetro, se añade la nueva entidad a la base de datos, se mapea a un DTO y se devuelve como resultado
    // Si el nulo, se devuelve nulo 
    public async Task<CarDriverDTO?> InsertCarDriver(CarDriver? newCarDriver)
    {
        if (newCarDriver != null)
        {
            EntityEntry<CarDriver> cardriver = _cardepocontext.CarDrivers.Add(newCarDriver);
            await _cardepocontext.SaveChangesAsync();
            CarDriver result = cardriver.Entity;

            var dto = new CarDriverDTO()
            {
                Id = result.Id,
                DateDrive = result.DateDrive,
                CarCDId = result.CarCDId,
                DriverCDId = result.DriverCDId
            };

            return dto;
        }
        else { return null; }
    }

    // Para actualizar una entidad. Para esto, se utiliza como parametro el Id de la entidad a manipular y el nuevo conductor
    // Si se encuentra la entidad con el Id, el se cambian los datos de la entidad por los de la nueva, se actualiza la entidad y se guardan los resultados, y se devuelve un DTO como resultado
    // Si no se encuentra, se devuelve nulo
    public async Task<CarDriverDTO?> UpdateCarDriver(int CarDriverId, CarDriver newCarDriver)
    {
        var result = await _cardepocontext.CarDrivers.FindAsync(CarDriverId);

        if (result != null)
        {
            result.DateDrive = newCarDriver.DateDrive;
            result.CarCDId = newCarDriver.CarCDId;
            result.DriverCDId = newCarDriver.DriverCDId;
            result.Car = newCarDriver.Car;
            result.Driver = newCarDriver.Driver;

            _cardepocontext.CarDrivers.Update(result);
            await _cardepocontext.SaveChangesAsync();

            var dto = new CarDriverDTO()
            {
                Id = result.Id,
                DateDrive = result.DateDrive,
                CarCDId = result.CarCDId,
                DriverCDId = result.DriverCDId
            };

            return dto;
        }
        else { return null; }
    }

    // Para borrar una entidad de la base de datos. Recibe el Id de la entidad a borrar.
    // Utilizando este Id, busca a la entidad. Si la encuentra, la borra de la base de datos.
    public async Task DeleteCarDriver(int CarDriverId)
    {
        var result = await _cardepocontext.CarDrivers.FindAsync(CarDriverId);
        if (result != null)
        {
            _cardepocontext.CarDrivers.Remove(result);
            await _cardepocontext.SaveChangesAsync();
        }
    }

    // Para comprobar si una entidad existe o no. 
    // Dependencia de su existencia, devuelve un true o un false
    public bool IfCarDriverExists(int CarDriverId)
    {
        return _cardepocontext.CarDrivers.Any(e => e.Id == CarDriverId);
    }
}