using CarDepo.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarDepo.Infrastructure.EntityProperties;

public class FineEntityTypeConfiguration : IEntityTypeConfiguration<Fine>
{
    public void Configure(EntityTypeBuilder<Fine> builder)
    {
        // Uno a muchos con Car
        builder
            .HasOne(e => e.Car)
            .WithMany()
            .HasForeignKey(e => e.CarId)
            .IsRequired();

        // Uno a muchos con Owner
        builder
            .HasOne(e => e.Owner)
            .WithMany()
            .HasForeignKey(e => e.OwnerId)
            .IsRequired();

        // Infromación inicial
        builder
            .HasData(
                new Fine { Id = 1, CarId = 1, OwnerId = 1, Date = DateOnly.Parse("2025-05-26"), Payed = true, Price = 45.30M , Description = "-" }
            );
    }
}