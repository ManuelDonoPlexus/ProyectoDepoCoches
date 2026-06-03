using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDepo.API.Models;

public class Fine
{
    [Key]
    public int Id { get; set; }
    public required decimal Price { get; set; }
    public required bool Payed { get; set; }
    public string? Description { get; set; }

    [DataType(DataType.Date)]
    public DateOnly Date { get; set; }

    // Owner
    [ForeignKey(nameof(Owner))]
    [Required]
    public required int OwnerId { get; set; }
    public virtual Owner? Owner { get; set; }

    // Owner
    [ForeignKey(nameof(Car))]
    [Required]
    public required int CarId { get; set; }
    public virtual Car? Car { get; set; }
}