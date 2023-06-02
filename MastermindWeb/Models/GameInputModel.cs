using System.ComponentModel.DataAnnotations;

namespace MastermindWeb.Models;

public class GameInputModel
{
    [Range(1, 8)]
    [Required]
    [Display(Name = "Code length:", Prompt = "<1,8>")]
    public int CodeLength { get; set; } = 4;

    [Range(2, 20)]
    [Required]
    [Display(Name = "Max turns count:", Prompt = "<2,20>")]
    public int TurnsCount { get; set; } = 10;

    [Display(Name = "Allow Duplicates:")]
    public bool AllowDuplicates { get; set; }
}