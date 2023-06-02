using MastermindCore.Entity;

namespace MastermindWeb.Models;

public class YourAttemptsModel
{
    public IList<Score> Scores { get; set; } = null!;
}