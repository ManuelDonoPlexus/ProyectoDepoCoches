using System.ComponentModel;
using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;

namespace CarDepo.Application.Services;

public class CarServiceDbContext
{
    private readonly CarDepoContext _context;

    public CarServiceDbContext(CarDepoContext context)
    {
        _context = context;
    }

    public async Task<bool> insertCarWithValidation(string license, int kms, int colorId, int ownerId, int makeId)
    {
        Car cartoadd = new Car
        {
            License = license,
            Kms = kms,
            ColorId = colorId,
            OwnerId = ownerId,
            MakeId = makeId,
        };

        if (!_context.Colors.Any(c => cartoadd.ColorId == c.Id)) { return false; }

        if (!_context.Owners.Any(c => cartoadd.OwnerId == c.Id)) { return false; }

        if (!_context.Makes.Any(c => cartoadd.MakeId == c.Id)) { return false; }

        await _context.Cars.AddAsync(cartoadd);
        await _context.SaveChangesAsync();
        return true;
    }
}