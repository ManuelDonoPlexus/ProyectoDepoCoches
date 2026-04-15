using CarDepo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CarDepo.API.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new CarDepoContext(
            serviceProvider.GetRequiredService<DbContextOptions<CarDepoContext>>()))
        {
            if (context.Cars.Any())
            {
                return;
            }
            context.SaveChanges();
        }
    }
}