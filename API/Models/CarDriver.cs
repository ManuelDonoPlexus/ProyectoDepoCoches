using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDepo.API.Models;

public class CarDriver
{
    [Key]
    public int Id { get; set; }
    public DateOnly DateDrive { get; set; }

    // Car
    [ForeignKey(nameof(Car))]
    [Required]
    public int CarCDId { get; set; }
    public Car Car { get; set; }
    
    // Conductor
    [ForeignKey(nameof(Driver))]
    [Required]
    public int DriverCDId { get; set; }
    public Driver Driver { get; set; }
}