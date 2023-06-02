using System.ComponentModel.DataAnnotations;

namespace MastermindWeb.Models;

public class PlayerRegisterModel
{
    [Required]
    [Display(Name = "Player name:")]
    [MinLength(3)]
    public string Name { get; set; } = "";

    [Required]
    [Display(Name = "Password:")]
    [DataType(DataType.Password)]
    [MinLength(8)]
    public string Password { get; set; } = "";

    [Required]
    [Display(Name = "Password again:")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; } = "";
}