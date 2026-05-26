using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarDepo.Infrastructure.Repositories;

public interface IColorRepository
{
    Task<Color?> InsertColor(Color? newColor);
    Task<Color?> GetColor(int ColorId);
    Task<IEnumerable<Color>> GetColors();
    Task DeleteColor(int ColorId);
    Task<Color?> UpdateColor(int ColorId, Color newColor);
    bool IfColorExists(int ColorId);
}

public class ColorRepository : IColorRepository
{
    private readonly CarDepoContext _cardepocontext;

    public ColorRepository(CarDepoContext context)
    {
        _cardepocontext = context;
    }

    public async Task<IEnumerable<Color>> GetColors()
    {
        return await _cardepocontext.Colors
                .AsNoTracking()
                .ToListAsync();
    }

    public async Task<Color?> GetColor(int ColorId)
    {
        return await _cardepocontext.Colors
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == ColorId);
    }

    public async Task<Color?> InsertColor(Color? newColor)
    {
        if (newColor != null)
        {
            EntityEntry<Color> color = _cardepocontext.Colors.Add(newColor);
            await _cardepocontext.SaveChangesAsync();
            return color.Entity;
        }
        else { return null; }

    }

    public async Task<Color?> UpdateColor(int ColorId, Color newColor)
    {
        var result = await _cardepocontext.Colors.FindAsync(ColorId);

        if (result != null)
        {
            result.Name = newColor.Name;
            await _cardepocontext.SaveChangesAsync();
            return result;
        }
        else { return null; }    
    }

    public async Task DeleteColor(int ColorId)
    {
        var result = await GetColor(ColorId);
        if (result != null)
        {
            _cardepocontext.Colors.Remove(result);
            await _cardepocontext.SaveChangesAsync();
        }
    }

    public bool IfColorExists(int ColorId)
    {
        return _cardepocontext.Colors.Any(e => e.Id == ColorId);
    }
}