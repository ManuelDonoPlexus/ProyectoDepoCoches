using CarDepo.API.DTOs.Make;
using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarDepo.Infrastructure.Repositories;
// Capa de repositorio (encargada de obtener y manipular la información)

// Interfaz que señala los metodos iniciales
public interface IMakeRepository
{
    Task<IEnumerable<Make>> GetMakes();
    Task<MakeDTO?> GetMake(int MakeId);
    Task<MakeDTO?> InsertMake(Make? newMake);
    Task<MakeDTO?> UpdateMake(int MakeId, Make newMake);
    Task DeleteMake(int MakeId);
    bool IfMakeExists(int MakeId);
}

// Repositorio que implementa la interfaz de repositorio
public class MakeRepository : IMakeRepository
{
    private readonly CarDepoContext _cardepocontext; // contexto de la base de datos

    // Define las anteriores variables para su uso, inicializando la clase.
    public MakeRepository(CarDepoContext context)
    {
        _cardepocontext = context;
    }

    // Obtiene todas las entidades de una tabla correspondiente con la entidad asocidada al repositorio
    public async Task<IEnumerable<Make>> GetMakes()
    {
        return await _cardepocontext.Makes
                .Include(m => m.FuelType)
                .AsNoTracking()
                .ToListAsync();
    }

    // Busca a la entidad que coincidan con el Id recibido como parametro en la Base de Datos
    // Si se encuentra, es mapeado a un DTO y devuelto como resultado
    // Si no se encuentra, se devuelve nulo 
    public async Task<MakeDTO?> GetMake(int MakeId)
    {
        Make? result = await _cardepocontext.Makes
                .Include(m => m.FuelType)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == MakeId);

        if (result != null)
        {
            var dto = new MakeDTO()
            {
                Id = result.Id,
                Name = result.Name,
                HorsePower = result.HorsePower,
                Price = result.Price,
                FuelTypeId = result.FuelTypeId
            };

            return dto;
        }
        else { return null; }
    }

    // Para insertar una nueva entidad. 
    // Si no es nulo el paranetro, se añade la nueva entidad a la base de datos, se mapea a un DTO y se devuelve como resultado
    // Si el nulo, se devuelve nulo 
    public async Task<MakeDTO?> InsertMake(Make? newMake)
    {
        if (newMake != null)
        {
            EntityEntry<Make> make = _cardepocontext.Makes.Add(newMake);
            await _cardepocontext.SaveChangesAsync();
            Make result = make.Entity;

            var dto = new MakeDTO()
            {
                Id = result.Id,
                Name = result.Name,
                HorsePower = result.HorsePower,
                Price = result.Price,
                FuelTypeId = result.FuelTypeId
            };

            return dto;
        }
        else { return null; }
    }

    // Para actualizar una entidad. Para esto, se utiliza como parametro el Id de la entidad a manipular y el nuevo conductor
    // Si se encuentra la entidad con el Id, el se cambian los datos de la entidad por los de la nueva, se actualiza la entidad y se guardan los resultados, y se devuelve un DTO como resultado
    // Si no se encuentra, se devuelve nulo
    public async Task<MakeDTO?> UpdateMake(int MakeId, Make newMake)
    {
        var result = await _cardepocontext.Makes.FindAsync(MakeId);

        if (result != null)
        {
            result.Name = newMake.Name;
            result.Price = newMake.Price;
            result.FuelTypeId = newMake.FuelTypeId;
            result.FuelType = newMake.FuelType;

            _cardepocontext.Makes.Update(result);
            await _cardepocontext.SaveChangesAsync();

            var dto = new MakeDTO()
            {
                Id = result.Id,
                Name = result.Name,
                HorsePower = result.HorsePower,
                Price = result.Price,
                FuelTypeId = result.FuelTypeId
            };

            return dto;
        }
        else { return null; }
    }

    // Para borrar una entidad de la base de datos. Recibe el Id de la entidad a borrar.
    // Utilizando este Id, busca a la entidad. Si la encuentra, la borra de la base de datos.
    public async Task DeleteMake(int MakeId)
    {
        var result = await _cardepocontext.Makes.FindAsync(MakeId);
        if (result != null)
        {
            _cardepocontext.Makes.Remove(result);
            await _cardepocontext.SaveChangesAsync();
        }
    }

    // Para comprobar si una entidad existe o no. 
    // Dependencia de su existencia, devuelve un true o un false
    public bool IfMakeExists(int MakeId)
    {
        return _cardepocontext.Makes.Any(e => e.Id == MakeId);
    }
}