using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CarDepoTest.Tests.Utils;

public class RepoClassTestUtil()
{
    public CarDepoContext ReturnFakeContext()
    {
        var options = new DbContextOptionsBuilder<CarDepoContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new CarDepoContext(options);

        Color colortest = new Color { Id = 1, Name = "Rojo" };
        FuelType fueltypetest = new FuelType { Id = 1, Name = "Petroleo" };
        Make maketest = new Make { Id = 1, Name = "TestMake", HorsePower = 100, Price = 10000.00M, FuelTypeId = 1 };
        Owner ownertest = new Owner { Id = 1, Name = "TestOwner", Nif = "N12345678", PhoneNumber = 999333666, DateEntry = DateOnly.Parse("2001-01-01"), EmailAddr = "testowner@test" };
        Car cartest = new Car { Id = 1, License = "1234-ABC", Kms = 100, ColorId = 1, MakeId = 1, OwnerId = 1 };
        Driver drivertest = new Driver { Id = 1, Name = "Abc", Dni = "123456789A", EmailAddr = "test@test.test", OwnerId = 1 };
        Fine finetest = new Fine { Id = 1, CarId = 1, OwnerId = 1, Date = DateOnly.Parse("2001-01-1"), Payed = true, Price = 10.00M, Description = "" };
        CarDriver cardrivertest = new CarDriver { Id = 1, DateDrive = DateOnly.Parse("2001-01-1"), CarCDId = 1, DriverCDId = 1 };

        context.Colors.Add(colortest);
        context.FuelTypes.Add(fueltypetest);
        context.Makes.Add(maketest);
        context.Owners.Add(ownertest);
        context.Cars.Add(cartest);
        context.Drivers.Add(drivertest);
        context.Fines.Add(finetest);
        context.CarDrivers.Add(cardrivertest);

        context.SaveChanges();
        return context;
    }
}