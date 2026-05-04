using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDepo.API.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}