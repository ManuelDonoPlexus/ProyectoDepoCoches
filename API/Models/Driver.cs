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
    [ForeignKey(nameof(Owner))]
    public required int OwnerId { get; set; } 
    [Required]
    public Owner Owner { get; set; }
    
    // Car
    [ForeignKey(nameof(Car))]
    public required int CarId { get; set; }    
    [Required]
    public Car Car { get; set; }

    // Car Drivers
    public List<CarDriver>? CarConductor { get; set; }
}