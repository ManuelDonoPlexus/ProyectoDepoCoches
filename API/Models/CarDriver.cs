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
    public int CarCDId { get; set; }
    [Required]
    public Car Car { get; set; }
    
    // Conductor
    [ForeignKey(nameof(Driver))]
    public int DriverCDId { get; set; }
    [Required]
    public Driver Driver { get; set; }
}