using Microsoft.EntityFrameworkCore;

namespace CarDepo.Infrastructure.Data
{
    public class CarDepoContext : DbContext
    {
        public CarDepoContext (DbContextOptions<CarDepoContext> options)
            :base(options)
        {            
        }

        public DbSet<API.Models.Car> Cars { get; set; } = default!;
        public DbSet<API.Models.CarDriver> CarConductors { get; set; } = default!;
        public DbSet<API.Models.Color> Colors { get; set; } = default!;
        public DbSet<API.Models.Driver> Drivers { get; set; } = default!;
        public DbSet<API.Models.Fine> Fines { get; set; } = default!;
        public DbSet<API.Models.FuelType> FuelTypes { get; set; } = default!;
        public DbSet<API.Models.Make> Makes { get; set; } = default!;
        public DbSet<API.Models.Owner> Owners { get; set; } = default!;
    }
}