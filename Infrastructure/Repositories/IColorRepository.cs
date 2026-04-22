using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarDepo.Infrastructure.Repositories;

public interface IColorRepository
{
    Task<Color> InsertColor(Color Color);
    Task<Color?> GetColor(int ColorId);
    Task<IEnumerable<Color>> GetColors();
    Task DeleteColor(int ColorId);
    Task UpdateColor(int ColorId, Color Color);
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

    public async Task<Color> InsertColor(Color Color)
    {
        EntityEntry<Color> color = _context.Colors.Add(Color);
        await _context.SaveChangesAsync();

        return color.Entity;
    }

    public async Task UpdateColor(int ColorId, Color Color)
    {
        if (ColorId == Color.Id)
        {
            _context.Entry(Color).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteColor(int ColorId)
    {
        var color = await _context.Colors.FindAsync(ColorId);
        if (color != null)
        {
            _context.Colors.Remove(color);
            await _context.SaveChangesAsync();
        }
    }

    public bool IfColorExists(int ColorId)
    {
        return _context.Colors.Any(e => e.Id == ColorId);
    }
}