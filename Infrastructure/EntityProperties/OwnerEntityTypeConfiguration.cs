using CarDepo.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarDepo.Infrastructure.EntityProperties;

public class OwnerEntityTypeConfiguration : IEntityTypeConfiguration<Owner>
{
    public void Configure(EntityTypeBuilder<Owner> builder)
    {
        builder
            .HasData(
                new Owner { Id = 1, Name = "Jarvis INC", Nif = "N79652124", PhoneNumber = 965123415, DateEntry = DateOnly.Parse("2007-03-14"), EmailAddr = "jarvis@company.inc" },
                new Owner { Id = 2, Name = "Donquer SL", Nif = "B12472965", PhoneNumber = 658120205, DateEntry = DateOnly.Parse("2010-04-07"), EmailAddr = "donquer@companiamania.sl" },
                new Owner { Id = 3, Name = "Luzcar SA", Nif = "A65212479", PhoneNumber = 912341415, DateEntry = DateOnly.Parse("2008-05-21"), EmailAddr = "luzcar@correo.sa" },
                new Owner { Id = 4, Name = "Cantar CB", Nif = "E12495276", PhoneNumber = 718916545, DateEntry = DateOnly.Parse("2004-07-30"), EmailAddr = "cantar@ascancions.cb" }
            );
    }
}