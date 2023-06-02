using System.ComponentModel.DataAnnotations;

namespace MastermindWeb.Models;

public class PlayerLoginModel
{
    [Required]
    [Display(Name = "Player name:")]
    public string Name { get; set; } = "";

    [Required]
    [Display(Name = "Password:")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = "";
}