namespace CarDepo.API.DTOs.Fine;

public class FineInsertDTO
{
    public int Id { get; set; }
    public required decimal Price { get; set; }
    public required bool Payed { get; set; }
    public string? Description { get; set; }
    public DateOnly Date { get; set; }
    public required int OwnerId { get; set; }
    public required int CarId { get; set; }
}