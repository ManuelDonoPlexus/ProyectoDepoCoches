using System.ComponentModel.DataAnnotations;

namespace CarDepo.API.Models;

public class Owner
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string NIF { get; set; }
    
    [DataType(DataType.PhoneNumber)]
    public int? PhoneNumber { get; set; }

    [DataType(DataType.Date)]
    public DateOnly DateEntry { get; set; }

    [DataType(DataType.EmailAddress)]
    public string? EmailAddr { get; set; }
}