using CarDepo.API.DTOs.Fine;
using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarDepo.Infrastructure.Repositories;

public interface IFineRepository
{
    Task<IEnumerable<Fine>> GetFines();
    Task<FineDTO?> GetFine(int FineId);
    Task<FineDTO?> InsertFine(Fine? newFine);
    Task<FineDTO?> UpdateFine(int FineId, Fine newFine);
    Task DeleteFine(int FineId);
    bool IfFineExists(int FineId);
}

public class FineRepository : IFineRepository
{
    private readonly CarDepoContext _cardepocontext;

    public FineRepository(CarDepoContext context)
    {
        _cardepocontext = context;
    }

    public async Task<IEnumerable<Fine>> GetFines()
    {
        return await _cardepocontext.Fines
                .Include(f => f.Owner)
                .Include(f => f.Car)
                .AsNoTracking()
                .ToListAsync();
    }

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

    public async Task DeleteFine(int FineId)
    {
        var result = await _cardepocontext.Fines.FindAsync(FineId);
        if (result != null)
        {
            _cardepocontext.Fines.Remove(result);
            await _cardepocontext.SaveChangesAsync();
        }
    }

    public bool IfFineExists(int FineId)
    {
        return _cardepocontext.Fines.Any(e => e.Id == FineId);
    }
}