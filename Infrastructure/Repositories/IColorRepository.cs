using CarDepo.API.DTOs.Color;
using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarDepo.Infrastructure.Repositories;
// Capa de repositorio (encargada de obtener y manipular la información)

// Interfaz que señala los metodos iniciales
public interface IColorRepository
{
    Task<IEnumerable<Color>> GetColors();
    Task<ColorDTO?> GetColor(int ColorId);
    Task<ColorDTO?> InsertColor(Color? newColor);
    Task<ColorDTO?> UpdateColor(int ColorId, Color newColor);
    Task DeleteColor(int ColorId);
    bool IfColorExists(int ColorId);
}

// Repositorio que implementa la interfaz de repositorio
public class ColorRepository : IColorRepository
{
    private readonly CarDepoContext _cardepocontext; // contexto de la base de datos

    // Define las anteriores variables para su uso, inicializando la clase.
    public ColorRepository(CarDepoContext context)
    {
        _cardepocontext = context;
    }

    // Obtiene todas las entidades de una tabla correspondiente con la entidad asocidada al repositorio
    public async Task<IEnumerable<Color>> GetColors()
    {
        return await _cardepocontext.Colors
                .AsNoTracking()
                .ToListAsync();
    }

    // Busca a la entidad que coincidan con el Id recibido como parametro en la Base de Datos
    // Si se encuentra, es mapeado a un DTO y devuelto como resultado
    // Si no se encuentra, se devuelve nulo 
    public async Task<ColorDTO?> GetColor(int ColorId)
    {
        Color? result = await _cardepocontext.Colors
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == ColorId);

        if (result != null)
        {
            var dto = new ColorDTO()
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
    public async Task<ColorDTO?> InsertColor(Color? newColor)
    {
        if (newColor != null)
        {
            EntityEntry<Color> color = _cardepocontext.Colors.Add(newColor);
            await _cardepocontext.SaveChangesAsync();
            Color result = color.Entity;

            var dto = new ColorDTO()
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
    public async Task<ColorDTO?> UpdateColor(int ColorId, Color newColor)
    {
        var result = await _cardepocontext.Colors.FindAsync(ColorId);

        if (result != null)
        {
            result.Name = newColor.Name;
            _cardepocontext.Colors.Update(result);
            await _cardepocontext.SaveChangesAsync();

            var dto = new ColorDTO()
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
    public async Task DeleteColor(int ColorId)
    {
        var result = await _cardepocontext.Colors.FindAsync(ColorId);
        if (result != null)
        {
            _cardepocontext.Colors.Remove(result);
            await _cardepocontext.SaveChangesAsync();
        }
    }

    // Para comprobar si una entidad existe o no. 
    // Dependencia de su existencia, devuelve un true o un false
    public bool IfColorExists(int ColorId)
    {
        return _cardepocontext.Colors.Any(e => e.Id == ColorId);
    }
}