using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDepo.API.Models;

public class Fine
{
    [Key]
    public int Id { get; set; }

    public required decimal Price { get; set; }

    public required bool Payed { get; set; }

    [DataType(DataType.Date)]
    public DateOnly Date { get; set; }

    // Owner
    [ForeignKey("Owner")]
    public required int OwnerId { get; set; }

    public required Owner Owner { get; set; }

    // Owner
    [ForeignKey("Car")]
    public required int CarId { get; set; }

    public required Car Car { get; set; }
}