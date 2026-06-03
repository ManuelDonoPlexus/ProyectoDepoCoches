namespace CarDepo.API.DTOs.Owner;

public class OwnerDTO
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Nif { get; set; }
    public int? PhoneNumber { get; set; }
    public DateOnly DateEntry { get; set; }
    public string? EmailAddr { get; set; }
}