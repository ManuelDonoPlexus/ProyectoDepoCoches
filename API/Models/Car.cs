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
    public required int Kms { get; set; }
    
    // Color
    [ForeignKey(nameof(Color))]
    [Required]
    public required int ColorId { get; set; }
    public virtual Color Color { get; set; }
    
    // Owner
    [ForeignKey(nameof(Owner))]
    [Required]
    public required int OwnerId { get; set; }
    public virtual Owner Owner { get; set; }
    
    // Make
    [ForeignKey(nameof(Make))]
    [Required]
    public required int MakeId { get; set; }
    public Make Make { get; set; }
}