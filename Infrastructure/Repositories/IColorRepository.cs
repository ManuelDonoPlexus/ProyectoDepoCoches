using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarDepo.Infrastructure.Repositories;

public interface IColorRepository
{
    Task<Color?> InsertColor(Color? Color);
    Task<Color?> GetColor(int ColorId);
    Task<IEnumerable<Color>> GetColors();
    Task DeleteColor(int ColorId);
    Task<Color?> UpdateColor(int ColorId, Color newColor);
    bool IfColorExists(int ColorId);
}

public class ColorRepository : IColorRepository
{
    private readonly CarDepoContext _context;

    public ColorRepository(CarDepoContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Color>> GetColors()
    {
        return await _context.Colors
                .AsNoTracking()
                .ToListAsync();
    }

    public async Task<Color?> GetColor(int ColorId)
    {
        return await _context.Colors
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == ColorId);
    }

    public async Task<Color?> InsertColor(Color? Color)
    {
        if (Color != null)
        {
            EntityEntry<Color> color = _context.Colors.Add(Color);
            await _context.SaveChangesAsync();
            return color.Entity;
        }
        else { return null; }

    }

    public async Task<Color?> UpdateColor(int ColorId, Color newColor)
    {
        var result = await _context.Colors.FindAsync(ColorId);

        if (result != null)
        {
            result.Name = newColor.Name;
            await _context.SaveChangesAsync();
            return result;
        }
        else { return null; }    
    }

    public async Task DeleteColor(int ColorId)
    {
        var result = await GetColor(ColorId);
        if (result != null)
        {
            _context.Colors.Remove(result);
            await _context.SaveChangesAsync();
        }
    }

    public bool IfColorExists(int ColorId)
    {
        return _context.Colors.Any(e => e.Id == ColorId);
    }
}