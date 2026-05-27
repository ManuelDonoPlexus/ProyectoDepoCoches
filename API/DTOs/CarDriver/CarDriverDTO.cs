namespace CarDepo.API.DTOs.CarDriver;

public class CarDriverDTO
{
    public int Id { get; set; }
    public DateOnly DateDrive { get; set; }
    public int CarCDId { get; set; }
    public int DriverCDId { get; set; }
}