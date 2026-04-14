using System.ComponentModel.DataAnnotations;

namespace CarDepo.API.Models;

public class Owner
{
    public int Id {get; set;}

    public string? Name {get; set;}

    [DataType(DataType.Date)]
    public DateOnly DateEntry {get; set;}

    [DataType(DataType.EmailAddress)]
    public string? EmailAddr {get; set;}

    public int PhoneNumber {get; set;}
}