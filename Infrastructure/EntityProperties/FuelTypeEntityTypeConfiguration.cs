using CarDepo.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarDepo.Infrastructure.EntityProperties;

public class FuelTypeEntityTypeConfiguration : IEntityTypeConfiguration<FuelType>
{
    public void Configure(EntityTypeBuilder<FuelType> builder)
    {
        // Información inicial
        builder
            .HasData(
                new FuelType { Id = 1, Name = "Petroleo" },
                new FuelType { Id = 2, Name = "Diesel" },
                new FuelType { Id = 3, Name = "Hibrido" },
                new FuelType { Id = 4, Name = "Electrico" }
            );
    }
}