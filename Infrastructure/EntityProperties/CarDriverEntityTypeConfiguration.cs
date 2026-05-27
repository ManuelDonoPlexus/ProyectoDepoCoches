using CarDepo.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarDepo.Infrastructure.EntityProperties;

public class CarDriverEntityTypeConfiguration : IEntityTypeConfiguration<CarDriver>
{
    public void Configure(EntityTypeBuilder<CarDriver> builder)
    {
        builder
            .HasOne(e => e.Driver)
            .WithMany()
            .HasForeignKey(e => e.DriverCDId)
            .IsRequired();
        
        builder
            .HasOne(e => e.Car)
            .WithMany()
            .HasForeignKey(e => e.CarCDId)
            .IsRequired();
        
        builder
            .HasData(
                new CarDriver { Id = 1, DateDrive = DateOnly.Parse("2017-06-17"), CarCDId = 1, DriverCDId = 3 },
                new CarDriver { Id = 2, DateDrive = DateOnly.Parse("2021-08-04"), CarCDId = 7, DriverCDId = 5 },
                new CarDriver { Id = 3, DateDrive = DateOnly.Parse("2019-11-22"), CarCDId = 5, DriverCDId = 4 },
                new CarDriver { Id = 4, DateDrive = DateOnly.Parse("2020-09-14"), CarCDId = 9, DriverCDId = 2 },
                new CarDriver { Id = 5, DateDrive = DateOnly.Parse("2022-03-22"), CarCDId = 3, DriverCDId = 1 },
                new CarDriver { Id = 6, DateDrive = DateOnly.Parse("2021-02-17"), CarCDId = 3, DriverCDId = 6 }
            );
    }
}
