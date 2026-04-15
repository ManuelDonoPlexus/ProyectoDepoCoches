using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDepo.API.Models;

public class Make
{
    [Key]
    public int Id { get; set; }

    public string? Name { get; set; }

    public int HorsePower { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    
    // Fuel
    
    [ForeignKey("FuelType")]
    public int FuelTypeId { get; set; }

    public required FuelType FuelType { get; set; }

}