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
    [ForeignKey(nameof(Color))]
    public required int ColorId { get; set; }
    [Required]
    public virtual Color Color { get; set; }
    
    // Owner
    [ForeignKey(nameof(Owner))]
    public required int OwnerId { get; set; }
    [Required]
    public virtual Owner Owner { get; set; }
    
    // Make
    [ForeignKey(nameof(Make))]
    public required int MakeId { get; set; }
    [Required]
    public Make Make { get; set; }
}