using CarDepo.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarDepo.Infrastructure.EntityProperties;

public class DriverEntityTypeConfiguration : IEntityTypeConfiguration<Driver>
{
    public void Configure(EntityTypeBuilder<Driver> builder)
    {
        // Uno a muchos con Owner
        builder
            .HasOne(e => e.Owner)
            .WithMany()
            .HasForeignKey(e => e.OwnerId)
            .IsRequired();
            
        // Información inicial
        builder
            .HasData(
                new Driver { Id = 1, Name = "Pepino", Dni = "78546932G", EmailAddr = "pepino@gmail.com", OwnerId = 2 },
                new Driver { Id = 2, Name = "Lucas", Dni = "47456968F", EmailAddr = "lucaselmoco@gmail.com", PhoneNumber = 985463127, OwnerId = 1 },
                new Driver { Id = 3, Name = "Mario", Dni = "69321453C", PhoneNumber = 985463127, OwnerId = 3 },
                new Driver { Id = 4, Name = "Ana", Dni = "49875214L", EmailAddr = "anadelasflores@email.com", OwnerId = 2 },
                new Driver { Id = 5, Name = "Regina", Dni = "45161314N", PhoneNumber = 617693541, OwnerId = 3 },
                new Driver { Id = 6, Name = "Maria", Dni = "94563214S", EmailAddr = "mariacarmenalojomora@panopticom.com", PhoneNumber = 874693125, OwnerId = 1 }
            );
    }
}







