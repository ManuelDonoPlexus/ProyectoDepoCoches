namespace CarDepo.API.DTOs.CarDriver;

public class CarDriverInsertDTO
{
    public int Id { get; set; }
    public DateOnly DateDrive { get; set; }
    public int CarCDId { get; set; }
    public int DriverCDId { get; set; }
}