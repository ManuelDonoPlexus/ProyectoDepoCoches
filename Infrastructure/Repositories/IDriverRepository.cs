using CarDepo.API.DTOs.Driver;
using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarDepo.Infrastructure.Repositories;
// Capa de repositorio (encargada de obtener y manipular la información)

// Interfaz que señala los metodos iniciales
public interface IDriverRepository
{
    Task<IEnumerable<Driver>> GetDrivers();
    Task<DriverDTO?> GetDriver(int DriverId);
    Task<DriverDTO?> InsertDriver(Driver? newDriver);
    Task<DriverDTO?> UpdateDriver(int DriverId, Driver newDriver);
    Task DeleteDriver(int DriverId);
    bool IfDriverExists(int DriverId);
}

// Repositorio que implementa la interfaz de repositorio
public class DriverRepository : IDriverRepository
{
    private readonly CarDepoContext _cardepocontext; // contexto de la base de datos

    // Define las anteriores variables para su uso, inicializando la clase.
    public DriverRepository(CarDepoContext context)
    {
        _cardepocontext = context;
    }

    // Obtiene todas las entidades de una tabla correspondiente con la entidad asocidada al repositorio
    public async Task<IEnumerable<Driver>> GetDrivers()
    {
        return await _cardepocontext.Drivers
                .Include(d => d.Owner)
                .AsNoTracking()
                .ToListAsync();
    }

    // Busca a la entidad que coincidan con el Id recibido como parametro en la Base de Datos
    // Si se encuentra, es mapeado a un DTO y devuelto como resultado
    // Si no se encuentra, se devuelve nulo 
    public async Task<DriverDTO?> GetDriver(int DriverId)
    {
        Driver? result = await _cardepocontext.Drivers
                .Include(d => d.Owner)
                .AsNoTracking()
                .FirstOrDefaultAsync(cd => cd.Id == DriverId);

        if (result != null)
        {
            var dto = new DriverDTO()
            {
                Id = result.Id,
                Name = result.Name,
                Dni = result.Dni,
                EmailAddr = result.EmailAddr,
                PhoneNumber = result.PhoneNumber,
                OwnerId = result.OwnerId
            };
            
            return dto;
        }
        else { return null; }
    }

    // Para insertar una nueva entidad. 
    // Si no es nulo el paranetro, se añade la nueva entidad a la base de datos, se mapea a un DTO y se devuelve como resultado
    // Si el nulo, se devuelve nulo 
    public async Task<DriverDTO?> InsertDriver(Driver? newDriver)
    {
        if (newDriver != null)
        {
            EntityEntry<Driver> driver = _cardepocontext.Drivers.Add(newDriver);
            await _cardepocontext.SaveChangesAsync();
            Driver result = driver.Entity;

            var dto = new DriverDTO()
            {
                Id = result.Id,
                Name = result.Name,
                Dni = result.Dni,
                EmailAddr = result.EmailAddr,
                PhoneNumber = result.PhoneNumber,
                OwnerId = result.OwnerId
            };

            return dto;
        }
        else { return null; }
    }

    // Para actualizar una entidad. Para esto, se utiliza como parametro el Id de la entidad a manipular y el nuevo conductor
    // Si se encuentra la entidad con el Id, el se cambian los datos de la entidad por los de la nueva, se actualiza la entidad y se guardan los resultados, y se devuelve un DTO como resultado
    // Si no se encuentra, se devuelve nulo
    public async Task<DriverDTO?> UpdateDriver(int DriverId, Driver newDriver)
    {
        var result = await _cardepocontext.Drivers.FindAsync(DriverId);

        if (result != null)
        {
            result.Name = newDriver.Name;
            result.Dni = newDriver.Dni;
            result.EmailAddr = newDriver.EmailAddr;
            result.PhoneNumber = newDriver.PhoneNumber;
            result.OwnerId = newDriver.OwnerId;
            result.Owner = newDriver.Owner;
            
            _cardepocontext.Drivers.Update(result);
            await _cardepocontext.SaveChangesAsync();

            var dto = new DriverDTO()
            {
                Id = result.Id,
                Name = result.Name,
                Dni = result.Dni,
                EmailAddr = result.EmailAddr,
                PhoneNumber = result.PhoneNumber,
                OwnerId = result.OwnerId
            };
            return dto;

        }
        else { return null; }
    }

    // Para borrar una entidad de la base de datos. Recibe el Id de la entidad a borrar.
    // Utilizando este Id, busca a la entidad. Si la encuentra, la borra de la base de datos.
    public async Task DeleteDriver(int DriverId)
    {
        var result = await _cardepocontext.Drivers.FindAsync(DriverId);
        if (result != null)
        {
            _cardepocontext.Drivers.Remove(result);
            await _cardepocontext.SaveChangesAsync();
        }
    }

    // Para comprobar si una entidad existe o no. 
    // Dependencia de su existencia, devuelve un true o un false
    public bool IfDriverExists(int DriverId)
    {
        return _cardepocontext.Drivers.Any(e => e.Id == DriverId);
    }
}