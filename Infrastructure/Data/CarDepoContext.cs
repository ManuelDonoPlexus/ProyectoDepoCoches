using Microsoft.EntityFrameworkCore;

namespace CarDepo.Infrastructure.Data
{
    public class CarDepoContext : DbContext
    {
        public CarDepoContext (DbContextOptions<CarDepoContext> options)
            :base(options)
        {            
        }

        public DbSet<CarDepo.API.Models.Car> Car { get; set; } = default!;
        public DbSet<CarDepo.API.Models.Owner> Owner { get; set; } = default!;
        public DbSet<CarDepo.API.Models.Make> Make { get; set; } = default!;
        public DbSet<CarDepo.API.Models.Color> Color { get; set; } = default!;
    }
}