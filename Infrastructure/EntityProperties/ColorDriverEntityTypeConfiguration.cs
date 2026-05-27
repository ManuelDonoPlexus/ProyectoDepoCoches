using CarDepo.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarDepo.Infrastructure.EntityProperties;

public class ColorDriverEntityTypeConfiguration : IEntityTypeConfiguration<Color>
{
    public void Configure(EntityTypeBuilder<Color> builder)
    {
        builder
            .HasData(
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
    }
}