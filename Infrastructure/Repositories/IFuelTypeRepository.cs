using CarDepo.API.DTOs.FuelType;
using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarDepo.Infrastructure.Repositories;
// Capa de repositorio (encargada de obtener y manipular la información)

// Interfaz que señala los metodos iniciales
public interface IFuelTypeRepository
{
    Task<IEnumerable<FuelType>> GetFuelTypes();
    Task<FuelTypeDTO?> GetFuelType(int FuelTypeId);
    Task<FuelTypeDTO?> InsertFuelType(FuelType? newFuelType);
    Task<FuelTypeDTO?> UpdateFuelType(int FuelTypeId, FuelType newFuelType);
    Task DeleteFuelType(int FuelTypeId);
    bool IfFuelTypeExists(int FuelTypeId);
}

// Repositorio que implementa la interfaz de repositorio
public class FuelTypeRepository : IFuelTypeRepository
{
    private readonly CarDepoContext _cardepocontext; // contexto de la base de datos

    // Define las anteriores variables para su uso, inicializando la clase.
    public FuelTypeRepository(CarDepoContext context)
    {
        _cardepocontext = context;
    }

    // Obtiene todas las entidades de una tabla correspondiente con la entidad asocidada al repositorio
    public async Task<IEnumerable<FuelType>> GetFuelTypes()
    {
        return await _cardepocontext.FuelTypes
                .AsNoTracking()
                .ToListAsync();
    }

    // Busca a la entidad que coincidan con el Id recibido como parametro en la Base de Datos
    // Si se encuentra, es mapeado a un DTO y devuelto como resultado
    // Si no se encuentra, se devuelve nulo 
    public async Task<FuelTypeDTO?> GetFuelType(int FuelTypeId)
    {
        FuelType? result = await _cardepocontext.FuelTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == FuelTypeId);

        if (result != null)
        {
            var dto = new FuelTypeDTO()
            {
                Id = result.Id,
                Name = result.Name
            };

            return dto;
        }
        else { return null; }
    }

    // Para insertar una nueva entidad. 
    // Si no es nulo el paranetro, se añade la nueva entidad a la base de datos, se mapea a un DTO y se devuelve como resultado
    // Si el nulo, se devuelve nulo 
    public async Task<FuelTypeDTO?> InsertFuelType(FuelType? newFuelType)
    {
        if (newFuelType != null)
        {
            EntityEntry<FuelType> fuelType = _cardepocontext.FuelTypes.Add(newFuelType);
            await _cardepocontext.SaveChangesAsync();
            FuelType? result = fuelType.Entity;
            
            var dto = new FuelTypeDTO()
            {
                Id = result.Id,
                Name = result.Name
            };

            return dto;
        }
        else { return null; }
    }

    // Para actualizar una entidad. Para esto, se utiliza como parametro el Id de la entidad a manipular y el nuevo conductor
    // Si se encuentra la entidad con el Id, el se cambian los datos de la entidad por los de la nueva, se actualiza la entidad y se guardan los resultados, y se devuelve un DTO como resultado
    // Si no se encuentra, se devuelve nulo
    public async Task<FuelTypeDTO?> UpdateFuelType(int FuelTypeId, FuelType newFuelType)
    {
        var result = await _cardepocontext.FuelTypes.FindAsync(FuelTypeId);

        if (result != null)
        {
            result.Name = newFuelType.Name;

            _cardepocontext.FuelTypes.Update(result);
            await _cardepocontext.SaveChangesAsync();
            
            var dto = new FuelTypeDTO()
            {
                Id = result.Id,
                Name = result.Name
            };

            return dto;
        }
        else { return null; }        
    }

    // Para borrar una entidad de la base de datos. Recibe el Id de la entidad a borrar.
    // Utilizando este Id, busca a la entidad. Si la encuentra, la borra de la base de datos.
    public async Task DeleteFuelType(int FuelTypeId)
    {
        var result = await _cardepocontext.FuelTypes.FindAsync(FuelTypeId);
        if (result != null)
        {
            _cardepocontext.FuelTypes.Remove(result);
            await _cardepocontext.SaveChangesAsync();
        }
    }

    // Para comprobar si una entidad existe o no. 
    // Dependencia de su existencia, devuelve un true o un false
    public bool IfFuelTypeExists(int FuelTypeId)
    {
        return _cardepocontext.FuelTypes.Any(e => e.Id == FuelTypeId);
    }
}