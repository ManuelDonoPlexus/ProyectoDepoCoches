using System.ComponentModel;
using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;

namespace CarDepo.Application.Services;

public class TestCarServiceDbContext
{
    private readonly CarDepoContext _context;

    public TestCarServiceDbContext(CarDepoContext context)
    {
        _context = context;
    }

    public async Task<bool> insertCarWithValidation(string license, int kms, int colorId, int ownerId, int makeId)
    {
        Car cartoadd = new Car
        {
            License = license,
            Kms = kms,
            ColorId = 7,
            OwnerId = 3,
            MakeId = 1,
        };

        if (!_context.Colors.Any(c => cartoadd.ColorId == c.Id))
        {
            return false;
        }

        if (!_context.Owners.Any(c => cartoadd.OwnerId == c.Id))
        {
            return false;
        }

        if (!_context.Colors.Any(c => cartoadd.MakeId == c.Id))
        {
            return false;
        }

        await _context.Cars.AddAsync(cartoadd);
        await _context.SaveChangesAsync();
        return true;
    }
}