using CarDepo.API.DTOs.Color;
using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarDepo.Infrastructure.Repositories;

public interface IColorRepository
{
    Task<IEnumerable<Color>> GetColors();
    Task<ColorDTO?> GetColor(int ColorId);
    Task<ColorDTO?> InsertColor(Color? newColor);
    Task<ColorDTO?> UpdateColor(int ColorId, Color newColor);
    Task DeleteColor(int ColorId);
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

    public async Task DeleteColor(int ColorId)
    {
        var result = await _cardepocontext.Colors.FindAsync(ColorId);
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