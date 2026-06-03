using CarDepo.API.DTOs.Owner;
using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarDepo.Infrastructure.Repositories;
// Capa de repositorio (encargada de obtener y manipular la información)

// Interfaz que señala los metodos iniciales
public interface IOwnerRepository
{
    Task<IEnumerable<Owner>> GetOwners();
    Task<OwnerDTO?> GetOwner(int OwnerId);
    Task<OwnerDTO?> InsertOwner(Owner? newOwner);
    Task<OwnerDTO?> UpdateOwner(int OwnerId, Owner Owner);
    Task DeleteOwner(int OwnerId);
    bool IfOwnerExists(int OwnerId);
}

// Repositorio que implementa la interfaz de repositorio
public class OwnerRepository : IOwnerRepository
{
    private readonly CarDepoContext _cardepocontext; // contexto de la base de datos

    // Define las anteriores variables para su uso, inicializando la clase.
    public OwnerRepository(CarDepoContext context)
    {
        _cardepocontext = context;
    }

    // Obtiene todas las entidades de una tabla correspondiente con la entidad asocidada al repositorio
    public async Task<IEnumerable<Owner>> GetOwners()
    {
        return await _cardepocontext.Owners
                .AsNoTracking()
                .ToListAsync();
    }

    // Busca a la entidad que coincidan con el Id recibido como parametro en la Base de Datos
    // Si se encuentra, es mapeado a un DTO y devuelto como resultado
    // Si no se encuentra, se devuelve nulo 
    public async Task<OwnerDTO?> GetOwner(int OwnerId)
    {
        Owner? result = await _cardepocontext.Owners
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == OwnerId);

        if (result != null)
        {
            var dto = new OwnerDTO()
            {
                Id = result.Id,
                Name = result.Name,
                Nif = result.Nif,
                PhoneNumber = result.PhoneNumber,
                DateEntry = result.DateEntry,
                EmailAddr = result.EmailAddr
            };

            return dto;
        }
        else { return null; }
    }

    // Para insertar una nueva entidad. 
    // Si no es nulo el paranetro, se añade la nueva entidad a la base de datos, se mapea a un DTO y se devuelve como resultado
    // Si el nulo, se devuelve nulo 
    public async Task<OwnerDTO?> InsertOwner(Owner? newOwner)
    {
        if (newOwner != null)
        {
            EntityEntry<Owner> owner = _cardepocontext.Owners.Add(newOwner);
            await _cardepocontext.SaveChangesAsync();
            Owner result = owner.Entity;

            var dto = new OwnerDTO()
            {
                Id = result.Id,
                Name = result.Name,
                Nif = result.Nif,
                PhoneNumber = result.PhoneNumber,
                DateEntry = result.DateEntry,
                EmailAddr = result.EmailAddr
            };

            return dto;
        }
        else { return null; }
    }

    // Para actualizar una entidad. Para esto, se utiliza como parametro el Id de la entidad a manipular y el nuevo conductor
    // Si se encuentra la entidad con el Id, el se cambian los datos de la entidad por los de la nueva, se actualiza la entidad y se guardan los resultados, y se devuelve un DTO como resultado
    // Si no se encuentra, se devuelve nulo
    public async Task<OwnerDTO?> UpdateOwner(int OwnerId, Owner newOwner)
    {
        var result = await _cardepocontext.Owners.FindAsync(OwnerId);

        if (result != null)
        {
            result.Name = newOwner.Name;
            result.Nif = newOwner.Nif;
            result.PhoneNumber = newOwner.PhoneNumber;
            result.DateEntry = newOwner.DateEntry;
            result.EmailAddr = newOwner.EmailAddr;
            
            _cardepocontext.Owners.Update(result);
            await _cardepocontext.SaveChangesAsync();

            var dto = new OwnerDTO()
            {
                Id = result.Id,
                Name = result.Name,
                Nif = result.Nif,
                PhoneNumber = result.PhoneNumber,
                DateEntry = result.DateEntry,
                EmailAddr = result.EmailAddr
            };

            return dto;
        }
        else { return null; }
    }

    // Para borrar una entidad de la base de datos. Recibe el Id de la entidad a borrar.
    // Utilizando este Id, busca a la entidad. Si la encuentra, la borra de la base de datos.
    public async Task DeleteOwner(int OwnerId)
    {
        var result = await _cardepocontext.Owners.FindAsync(OwnerId);
        if (result != null)
        {
            _cardepocontext.Owners.Remove(result);
            await _cardepocontext.SaveChangesAsync();
        }
    }

    // Para comprobar si una entidad existe o no. 
    // Dependencia de su existencia, devuelve un true o un false
    public bool IfOwnerExists(int OwnerId)
    {
        return _cardepocontext.Owners.Any(e => e.Id == OwnerId);
    }
}