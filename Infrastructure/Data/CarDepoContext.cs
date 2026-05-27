using CarDepo.API.Models;
using Microsoft.EntityFrameworkCore;
using CarDepo.Infrastructure.EntityProperties;

namespace CarDepo.Infrastructure.Data
{
    // Esta clase esta encargada de la construcción de la sesión con la base de datos que sera usada por los repositorios y controladores del programa.   
    public class CarDepoContext(DbContextOptions<CarDepoContext> options) : DbContext(options)
    {

        public DbSet<Car> Cars { get; set; } = default!;
        public DbSet<CarDriver> CarDrivers { get; set; } = default!;
        public DbSet<Color> Colors { get; set; } = default!;
        public DbSet<Driver> Drivers { get; set; } = default!;
        public DbSet<Fine> Fines { get; set; } = default!;
        public DbSet<FuelType> FuelTypes { get; set; } = default!;
        public DbSet<Make> Makes { get; set; } = default!;
        public DbSet<Owner> Owners { get; set; } = default!;
        public DbSet<User> Users { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new CarEntityTypeConfiguration().Configure(modelBuilder.Entity<Car>());
            new CarDriverEntityTypeConfiguration().Configure(modelBuilder.Entity<CarDriver>());
            new ColorDriverEntityTypeConfiguration().Configure(modelBuilder.Entity<Color>());
            new DriverEntityTypeConfiguration().Configure(modelBuilder.Entity<Driver>());
            new FineEntityTypeConfiguration().Configure(modelBuilder.Entity<Fine>());
            new FuelTypeEntityTypeConfiguration().Configure(modelBuilder.Entity<FuelType>());
            new MakeEntityTypeConfiguration().Configure(modelBuilder.Entity<Make>());
            new OwnerEntityTypeConfiguration().Configure(modelBuilder.Entity<Owner>());
            new UserEntityTypeConfiguration().Configure(modelBuilder.Entity<User>());
        }
    }
}

