using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDepo.API.Models;

public class Car
{
    [Key]
    public int Id { get; set; }

    public required string License { get; set; }

    [DefaultValue(0)]
    public required int KMs { get; set; }
    
    // Color

    [ForeignKey("Color")]
    public required int ColorId { get; set; }
    public required Color Color { get; set; }
    
    // Owner

    [ForeignKey("Owner")]
    public required int OwnerId { get; set; }
    public required Owner Owner { get; set; }
    
    // Make

    [ForeignKey("Make")]
    public required int MakeId { get; set; }
    public required Make Make { get; set; }

    // Car Drivers

    public List<CarDriver>? CarConductors { get; set; }
}