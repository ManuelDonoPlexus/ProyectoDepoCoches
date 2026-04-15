using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDepo.API.Models;

public class Driver
{
    [Key]
    public required int Id { get; set; }

    public required string Name { get; set; }
    public required string Dni { get; set; }

    public string? EmailAddr { get; set; }

    public int? PhoneNumber { get; set; }


    // Owner
    [ForeignKey("Owner")]
    public required int OwnerId { get; set; }

    public required Owner Owner { get; set; }
    
    // Car
    [ForeignKey("Car")]
    public required int CarId { get; set; }
    
    public required Car Car { get; set; }

    // Car Drivers
    public List<CarDriver>? CarConductor { get; set; }
}