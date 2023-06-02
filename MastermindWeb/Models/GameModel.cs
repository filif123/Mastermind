using MastermindCore;
using MastermindCore.Entity;
#pragma warning disable CS8618

namespace MastermindWeb.Models;

public class GameModel
{
    public Game Game { get; set; }

    public IList<Score> Scores { get; set; }

    public IList<Rating> Ratings { get; set; }

    public float AverageRating { get; set; }

    public CommentsModel Comments { get; set; }

    public Player Player { get; set; }

    public CodePeg[] Codes { get; set; }

    public int OwnRating { get; set; }
}

#pragma warning restore CS8618