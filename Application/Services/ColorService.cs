using CarDepo.API.DTOs.Color;
using CarDepo.API.Models;
using CarDepo.Infrastructure.Repositories;

namespace CarDepo.Application.Services;

public class ColorService : IColorRepository
{
    private readonly IColorRepository _colorrepo;

    public ColorService(IColorRepository colorrepo)
    {
        _colorrepo = colorrepo;
    }

    public async Task<IEnumerable<Color>> GetColors()
    {
        return await _colorrepo.GetColors();
    }

    public async Task<ColorDTO?> GetColor(int ColorId)
    {
        return await _colorrepo.GetColor(ColorId);
    }

    public async Task<ColorDTO?> InsertColor(Color? newColor)
    {
        return await _colorrepo.InsertColor(newColor);
    }

    public async Task<ColorDTO?> UpdateColor(int ColorId, Color newColor)
    {
        return await _colorrepo.UpdateColor(ColorId, newColor);
    }
    
    public async Task DeleteColor(int ColorId)
    {
        await _colorrepo.DeleteColor(ColorId);
    }

    public bool IfColorExists(int ColorId)
    {
        return _colorrepo.IfColorExists(ColorId);
    }

}