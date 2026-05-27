namespace CarDepo.API.DTOs.Driver;

public class DriverGetDTO
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Dni { get; set; }
    public string? EmailAddr { get; set; }
    public int? PhoneNumber { get; set; }
    public required int OwnerId { get; set; } 
}