namespace CarDepo.API.DTOs.Make;

public class MakeDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int HorsePower { get; set; }
    public decimal Price { get; set; }
    public required int FuelTypeId { get; set; }

}