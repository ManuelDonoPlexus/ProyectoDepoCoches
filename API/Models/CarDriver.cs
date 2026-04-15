using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDepo.API.Models;

public class CarDriver
{
    [Key]
    public int Id { get; set; }

    // Car

    [ForeignKey("Car")]
    public required int CarId { get; set; }
    public required Car Car { get; set; }
    
    // Conductor

    [ForeignKey("Driver")]
    public required int DriverId { get; set; }
    public required Driver Driver { get; set; }
}