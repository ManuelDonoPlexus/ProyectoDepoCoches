using CarDepo.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CarDepo.Infrastructure.Data
{
    public class CarDepoContext(DbContextOptions<CarDepoContext> options) : DbContext(options)
    {
        public DbSet<API.Models.Car> Cars { get; set; } = default!;
        public DbSet<API.Models.CarDriver> CarConductors { get; set; } = default!;
        public DbSet<API.Models.Color> Colors { get; set; } = default!;
        public DbSet<API.Models.Driver> Drivers { get; set; } = default!;
        public DbSet<API.Models.Fine> Fines { get; set; } = default!;
        public DbSet<API.Models.FuelType> FuelTypes { get; set; } = default!;
        public DbSet<API.Models.Make> Makes { get; set; } = default!;
        public DbSet<API.Models.Owner> Owners { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Make 

            modelBuilder.Entity<Make>()
                .HasOne(e => e.FuelType)
                .WithMany()
                .HasForeignKey(e => e.FuelTypeId)
                .IsRequired();

            // Driver

            modelBuilder.Entity<Driver>()
                .HasOne(e => e.Car)
                .WithMany()
                .HasForeignKey(e => e.CarId)
                .IsRequired();

            modelBuilder.Entity<Driver>()
                .HasOne(e => e.Owner)
                .WithMany()
                .HasForeignKey(e => e.OwnerId)
                .IsRequired();

            // Car

            modelBuilder.Entity<Car>()
                .HasOne(e => e.Color)
                .WithMany()
                .HasForeignKey(e => e.ColorId)
                .IsRequired();

            modelBuilder.Entity<Car>()
                .HasOne(e => e.Make)
                .WithMany()
                .HasForeignKey(e => e.MakeId)
                .IsRequired();

            modelBuilder.Entity<Car>()
                .HasOne(e => e.Owner)
                .WithMany()
                .HasForeignKey(e => e.OwnerId)
                .IsRequired();

            // CarDriver

            modelBuilder.Entity<CarDriver>()
                .HasOne(e => e.Driver)
                .WithMany()
                .HasForeignKey(e => e.DriverCDId)
                .IsRequired();

            modelBuilder.Entity<CarDriver>()
                .HasOne(e => e.Car)
                .WithMany()
                .HasForeignKey(e => e.CarCDId)
                .IsRequired();

            modelBuilder.Entity<FuelType>().HasData(
                new FuelType { Id = 1, Name = "Petroleo" },
                new FuelType { Id = 2, Name = "Diesel" },
                new FuelType { Id = 3, Name = "Hibrido" },
                new FuelType { Id = 4, Name = "Electrico" }
            );

            modelBuilder.Entity<Color>().HasData(
                new Color { Id = 1, Name = "Rojo" },
                new Color { Id = 2, Name = "Azul" },
                new Color { Id = 3, Name = "Verde" },
                new Color { Id = 4, Name = "Naranja" },
                new Color { Id = 5, Name = "Amarillo" },
                new Color { Id = 6, Name = "Celeste" },
                new Color { Id = 7, Name = "Purpura" },
                new Color { Id = 8, Name = "Rosa" },
                new Color { Id = 9, Name = "Negro" },
                new Color { Id = 10, Name = "Blanco" }
            );

            modelBuilder.Entity<Owner>().HasData(
                new Owner { Id = 1, Name = "Jarvis INC", NIF = "N79652124", PhoneNumber = 965123415, DateEntry = DateOnly.Parse("2007-03-14"), EmailAddr = "jarvis@company.inc" },
                new Owner { Id = 2, Name = "Donquer SL", NIF = "B12472965", PhoneNumber = 658120205, DateEntry = DateOnly.Parse("2010-04-07"), EmailAddr = "donquer@companiamania.sl" },
                new Owner { Id = 3, Name = "Luzcar SA", NIF = "A65212479", PhoneNumber = 912341415, DateEntry = DateOnly.Parse("2008-05-21"), EmailAddr = "luzcar@correo.sa" },
                new Owner { Id = 4, Name = "Cántar CB", NIF = "E12495276", PhoneNumber = 718916545, DateEntry = DateOnly.Parse("2004-07-30"), EmailAddr = "cantar@ascancions.cb" }
            );

            modelBuilder.Entity<Make>().HasData(
                new Make { Id = 1, Name = "Mercedes-Benz A 200", HorsePower = 163, Price = 25632.21M, FuelTypeId = 1 },
                new Make { Id = 2, Name = "Volvo V60 2.0 T6", HorsePower = 316, Price = 20545.85M, FuelTypeId = 3 },
                new Make { Id = 3, Name = "Hyundai I20 1.0 TGDI", HorsePower = 118, Price = 16697.64M, FuelTypeId = 2 }
            );

            modelBuilder.Entity<Driver>().HasData(
                new Driver { Id = 1, Name = "Pepino", Dni = "78546932G", OwnerId = 2, CarId = 3 },
                new Driver { Id = 2, Name = "Lucas", Dni = "47456968F", OwnerId = 1, CarId = 2 },
                new Driver { Id = 3, Name = "Mario", Dni = "69321453C", OwnerId = 3, CarId = 1 },
                new Driver { Id = 4, Name = "Ana", Dni = "49875214L", OwnerId = 2, CarId = 1 },
                new Driver { Id = 5, Name = "Regina", Dni = "45161314N", OwnerId = 3, CarId = 4 },
                new Driver { Id = 6, Name = "Maria", Dni = "94563214S", OwnerId = 1, CarId = 5 }
            );

            modelBuilder.Entity<Car>().HasData(
                new Car { Id = 1, License = "8742-GHX", KMs = 123, ColorId = 7, MakeId = 2, OwnerId = 1 },
                new Car { Id = 2, License = "7854-ASD", KMs = 103, ColorId = 9, MakeId = 1, OwnerId = 3 },
                new Car { Id = 3, License = "4152-RTE", KMs = 167, ColorId = 2, MakeId = 1, OwnerId = 2 },
                new Car { Id = 4, License = "6769-QWT", KMs = 263, ColorId = 1, MakeId = 2, OwnerId = 2 },
                new Car { Id = 5, License = "4157-NMD", KMs = 317, ColorId = 3, MakeId = 3, OwnerId = 1 },
                new Car { Id = 6, License = "3437-PLO", KMs = 401, ColorId = 4, MakeId = 3, OwnerId = 2 },
                new Car { Id = 7, License = "6167-AUD", KMs = 320, ColorId = 5, MakeId = 1, OwnerId = 2 },
                new Car { Id = 8, License = "5124-OIY", KMs = 115, ColorId = 6, MakeId = 2, OwnerId = 1 },
                new Car { Id = 9, License = "3112-THE", KMs = 154, ColorId = 8, MakeId = 1, OwnerId = 3 }
            );

            modelBuilder.Entity<CarDriver>().HasData(
                new CarDriver { Id = 1, DateDrive = DateOnly.Parse("2017-06-17"), CarCDId = 1, DriverCDId = 3 },
                new CarDriver { Id = 2, DateDrive = DateOnly.Parse("2021-08-04"), CarCDId = 7, DriverCDId = 5 },
                new CarDriver { Id = 3, DateDrive = DateOnly.Parse("2019-11-22"), CarCDId = 5, DriverCDId = 4 },
                new CarDriver { Id = 4, DateDrive = DateOnly.Parse("2020-09-14"), CarCDId = 9, DriverCDId = 2 },
                new CarDriver { Id = 5, DateDrive = DateOnly.Parse("2022-03-22"), CarCDId = 3, DriverCDId = 1 }
            );
        }
    }
}

