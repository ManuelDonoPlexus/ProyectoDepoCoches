namespace CarDepo.API.DTOs.Car;

public class CarGetDTO
{
    public int Id { get; set; }
    public required string License { get; set; }
    public required int Kms { get; set; }
    public required int ColorId { get; set; }
    public required int OwnerId { get; set; }
    public required int MakeId { get; set; }
}