using CarDepo.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarDepo.Infrastructure.EntityProperties;

public class FineEntityTypeConfiguration : IEntityTypeConfiguration<Fine>
{
    public void Configure(EntityTypeBuilder<Fine> builder)
    {
        builder
            .HasData(
                new Fine { Id = 1, CarId = 1, OwnerId = 1, Date = DateOnly.Parse("2025-05-26"), Payed = true, Price = 45.30M , Description = "-" }
            );
    }
}