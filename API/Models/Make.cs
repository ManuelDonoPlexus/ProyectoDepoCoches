using System.ComponentModel.DataAnnotations.Schema;

namespace CarDepo.API.Models;

public class Make
{
    public int Id {get; set;} 
    
    public string? Name {get; set;}
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price {get; set;}
    
    public int HorsePower {get; set;}
}