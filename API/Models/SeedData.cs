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
            if (context.Make.Any() && context.Owner.Any() && context.Car.Any())
            {
                return;
            }
            context.Make.AddRange(
                new Make
                {
                    Id = 1,
                    Name = "Mercedes-Benz A 200",
                    Price = 27990,
                    HorsePower = 163
                },
                new Make
                {
                    Id = 2,
                    Name = "Volvo V60 2.0 T6",
                    Price = 49990,
                    HorsePower = 316
                },
                new Make
                {
                    Id = 3,
                    Name = "Hyundai I20 1.0 TGDI",
                    Price = 16990,
                    HorsePower = 118
                }
            );
            context.Owner.AddRange(
                new Owner
                {
                    Id = 1,
                    Name = "Manolito",
                    DateEntry = DateOnly.Parse("2007-03-14"),
                    EmailAddr = "manolitomanolito@gmail.com",
                    PhoneNumber = 981236541
                },
                new Owner
                {
                    Id = 2,
                    Name = "Pepino",
                    DateEntry = DateOnly.Parse("2013-06-27"),
                    EmailAddr = "pepino@pepino.pepino",
                    PhoneNumber = 789645132
                },
                new Owner
                {
                    Id = 3,
                    Name = "Benjamin",
                    DateEntry = DateOnly.Parse("2004-09-02"),
                    EmailAddr = "benjaminelionardo@gmail.com",
                    PhoneNumber = 961247142
                }
            );
            context.Color.AddRange(
                new Color
                {
                    Id = 1,
                    Name = "Rojo",
                    HexCode = "#ff0000"
                },
                new Color
                {
                    Id = 2,
                    Name = "Azul",
                    HexCode = "#0000ff"
                },
                new Color
                {
                    Id = 3,
                    Name = "Verde",
                    HexCode = "#00ff00"
                },
                new Color
                {
                    Id = 4,
                    Name = "Naranja",
                    HexCode = "#ff9900"
                },
                new Color
                {
                    Id = 5,
                    Name = "Amarillo",
                    HexCode = "#ffff00"
                },
                new Color
                {
                    Id = 6,
                    Name = "Celeste",
                    HexCode = "#00ffff"
                },
                new Color
                {
                    Id = 7,
                    Name = "Purpura",
                    HexCode = "#aa00ff"
                },
                new Color
                {
                    Id = 8,
                    Name = "Rosa",
                    HexCode = "#ff00bb"
                }, 
                new Color
                {
                    Id = 9,
                    Name = "Negro",
                    HexCode = "#000000"
                }, 
                new Color
                {
                    Id = 10,
                    Name = "Blanco",
                    HexCode = "#ffffff"
                }
            );
            context.Car.AddRange(
                new Car
                {
                    Id = 1,
                    License = "ASCVFG97",
                    Kms = 147,
                    OwnerId = 1,
                    MakeId = 2,
                    ColorId = 9
                },
                new Car
                {
                    Id = 2,
                    License = "7896KLJ3",
                    Kms = 451,
                    OwnerId = 3,
                    MakeId = 3,
                    ColorId = 7
                },
                new Car
                {
                    Id = 3,
                    License = "4A4G37LN",
                    Kms = 317,
                    OwnerId = 2,
                    MakeId = 1,
                    ColorId = 4
                },
                new Car
                {
                    Id = 4,
                    License = "PLO0923IK",
                    Kms = 287,
                    OwnerId = 1,
                    MakeId = 3,
                    ColorId = 5
                },
                new Car
                {
                    Id = 5,
                    License = "IJK789NL",
                    Kms = 173,
                    OwnerId = 3,
                    MakeId = 3,
                    ColorId = 3
                }
            );
            context.SaveChanges();
        }
    }
}