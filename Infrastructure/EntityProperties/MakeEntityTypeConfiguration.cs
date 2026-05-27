using CarDepo.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarDepo.Infrastructure.EntityProperties;

public class MakeEntityTypeConfiguration : IEntityTypeConfiguration<Make>
{
    public void Configure(EntityTypeBuilder<Make> builder)
    {
        builder
            .HasOne(e => e.FuelType)
            .WithMany()
            .HasForeignKey(e => e.FuelTypeId)
            .IsRequired();
        
        builder
            .HasData(
                new Make { Id = 1, Name = "Mercedes-Benz A 200", HorsePower = 163, Price = 25632.21M, FuelTypeId = 1 },
                new Make { Id = 2, Name = "Volvo V60 2.0 T6", HorsePower = 316, Price = 20545.85M, FuelTypeId = 3 },
                new Make { Id = 3, Name = "Hyundai I20 1.0 TGDI", HorsePower = 118, Price = 16697.64M, FuelTypeId = 2 }
            );
    }
}