using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDepo.API.Models;

public class Driver
{
    [Key]
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Dni { get; set; }
    [DataType(DataType.EmailAddress)]
    public string? EmailAddr { get; set; }
    [DataType(DataType.PhoneNumber)]
    public int? PhoneNumber { get; set; }

    // Owner
    [ForeignKey(nameof(Owner))]
    public required int OwnerId { get; set; } 
    [Required]
    public Owner Owner { get; set; }
}