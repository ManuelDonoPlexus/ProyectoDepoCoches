using CarDepo.API.DTOs.Fine;
using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarDepo.Infrastructure.Repositories;
// Capa de repositorio (encargada de obtener y manipular la información)

// Interfaz que señala los metodos iniciales
public interface IFineRepository
{
    Task<IEnumerable<Fine>> GetFines();
    Task<FineDTO?> GetFine(int FineId);
    Task<FineDTO?> InsertFine(Fine? newFine);
    Task<FineDTO?> UpdateFine(int FineId, Fine newFine);
    Task DeleteFine(int FineId);
    bool IfFineExists(int FineId);
}

// Repositorio que implementa la interfaz de repositorio
public class FineRepository : IFineRepository
{
    private readonly CarDepoContext _cardepocontext; // contexto de la base de datos

    // Define las anteriores variables para su uso, inicializando la clase.
    public FineRepository(CarDepoContext context)
    {
        _cardepocontext = context;
    }

    // Obtiene todas las entidades de una tabla correspondiente con la entidad asocidada al repositorio
    public async Task<IEnumerable<Fine>> GetFines()
    {
        return await _cardepocontext.Fines
                .Include(f => f.Owner)
                .Include(f => f.Car)
                .AsNoTracking()
                .ToListAsync();
    }

    // Busca a la entidad que coincidan con el Id recibido como parametro en la Base de Datos
    // Si se encuentra, es mapeado a un DTO y devuelto como resultado
    // Si no se encuentra, se devuelve nulo 
    public async Task<FineDTO?> GetFine(int FineId)
    {
        Fine? result = await _cardepocontext.Fines
                .Include(f => f.Owner)
                .Include(f => f.Car)
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == FineId);

        if (result != null)
        {
            var dto = new FineDTO()
            {
                Id = result.Id,
                Price = result.Price,
                Payed = result.Payed,
                Description = result.Description,
                Date = result.Date,
                OwnerId = result.OwnerId,
                CarId = result.CarId
            };

            return dto;
        }
        else { return null; }
    }

    // Para insertar una nueva entidad. 
    // Si no es nulo el paranetro, se añade la nueva entidad a la base de datos, se mapea a un DTO y se devuelve como resultado
    // Si el nulo, se devuelve nulo 
    public async Task<FineDTO?> InsertFine(Fine? newFine)
    {
        if (newFine != null)
        {
            EntityEntry<Fine> fine = _cardepocontext.Fines.Add(newFine);
            await _cardepocontext.SaveChangesAsync();
            Fine? result = fine.Entity;

            var dto = new FineDTO()
            {
                Id = result.Id,
                Price = result.Price,
                Payed = result.Payed,
                Description = result.Description,
                Date = result.Date,
                OwnerId = result.OwnerId,
                CarId = result.CarId
            };

            return dto;
        }
        else { return null; }
    }

    // Para actualizar una entidad. Para esto, se utiliza como parametro el Id de la entidad a manipular y el nuevo conductor
    // Si se encuentra la entidad con el Id, el se cambian los datos de la entidad por los de la nueva, se actualiza la entidad y se guardan los resultados, y se devuelve un DTO como resultado
    // Si no se encuentra, se devuelve nulo
    public async Task<FineDTO?> UpdateFine(int FineId, Fine newFine)
    {
        var result = await _cardepocontext.Fines.FindAsync(FineId);

        if (result != null)
        {
            result.Date = newFine.Date;
            result.Description = newFine.Description;
            result.Price = newFine.Price;
            result.Payed = newFine.Payed;
            result.OwnerId = newFine.OwnerId;
            result.CarId = newFine.CarId;
            result.Owner = newFine.Owner;
            result.Car = newFine.Car;

            _cardepocontext.Fines.Update(result);
            await _cardepocontext.SaveChangesAsync();

            var dto = new FineDTO()
            {
                Id = result.Id,
                Price = result.Price,
                Payed = result.Payed,
                Description = result.Description,
                Date = result.Date,
                OwnerId = result.OwnerId,
                CarId = result.CarId
            };

            return dto;
        }
        else { return null; }

    }

    // Para borrar una entidad de la base de datos. Recibe el Id de la entidad a borrar.
    // Utilizando este Id, busca a la entidad. Si la encuentra, la borra de la base de datos.
    public async Task DeleteFine(int FineId)
    {
        var result = await _cardepocontext.Fines.FindAsync(FineId);
        if (result != null)
        {
            _cardepocontext.Fines.Remove(result);
            await _cardepocontext.SaveChangesAsync();
        }
    }

    // Para comprobar si una entidad existe o no. 
    // Dependencia de su existencia, devuelve un true o un false
    public bool IfFineExists(int FineId)
    {
        return _cardepocontext.Fines.Any(e => e.Id == FineId);
    }
}