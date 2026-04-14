using System.ComponentModel.DataAnnotations.Schema;

namespace CarDepo.API.Models;

public class Car
{
    public int Id { get; set; }

    public string? License { get; set; }

    public int Kms { get; set; }

    [ForeignKey("Owner")]
    public int OwnerId {get; set;}
    public Owner? Owner {get; set;}

    [ForeignKey("Make")]
    public int MakeId {get; set;}
    public Make? Make {get; set;}

    [ForeignKey("Color")]
    public int ColorId {get; set;}
    public int Color {get; set;}

}