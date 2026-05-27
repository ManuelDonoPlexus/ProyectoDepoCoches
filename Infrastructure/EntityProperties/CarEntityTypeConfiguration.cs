using CarDepo.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarDepo.Infrastructure.EntityProperties;

public class CarEntityTypeConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder
            .HasOne(e => e.Color)
            .WithMany()
            .HasForeignKey(e => e.ColorId)
            .IsRequired();

        builder
            .HasOne(e => e.Make)
            .WithMany()
            .HasForeignKey(e => e.MakeId)
            .IsRequired();

        builder
            .HasOne(e => e.Owner)
            .WithMany()
            .HasForeignKey(e => e.OwnerId)
            .IsRequired();
        
        builder
            .HasData(
                new Car { Id = 1, License = "8742-GHX", Kms = 123, ColorId = 7, MakeId = 2, OwnerId = 1 },
                new Car { Id = 2, License = "7854-ASD", Kms = 103, ColorId = 9, MakeId = 1, OwnerId = 3 },
                new Car { Id = 3, License = "4152-RTE", Kms = 167, ColorId = 2, MakeId = 1, OwnerId = 2 },
                new Car { Id = 4, License = "6769-QWT", Kms = 263, ColorId = 1, MakeId = 2, OwnerId = 2 },
                new Car { Id = 5, License = "4157-NMD", Kms = 317, ColorId = 3, MakeId = 3, OwnerId = 1 },
                new Car { Id = 6, License = "3437-PLO", Kms = 401, ColorId = 4, MakeId = 3, OwnerId = 2 },
                new Car { Id = 7, License = "6167-AUD", Kms = 320, ColorId = 5, MakeId = 1, OwnerId = 2 },
                new Car { Id = 8, License = "5124-OIY", Kms = 115, ColorId = 6, MakeId = 2, OwnerId = 1 },
                new Car { Id = 9, License = "3112-THE", Kms = 154, ColorId = 8, MakeId = 1, OwnerId = 3 }
            );
    }
}