using MastermindCore.Entity;
using Microsoft.EntityFrameworkCore;

namespace MastermindCore.Service.SqlDatabase;

public class SqlScoreService : IScoreService
{
    /// <inheritdoc />
    public void Add(Score score)
    {
        using var context = new MastermindDbContext();
        context.Scores.Add(score);
        context.SaveChanges();
    }

    /// <inheritdoc />
    public IList<Score> GetTopThree()
    {
        using var context = new MastermindDbContext();
        return context.Scores.Include(t => t.Player).OrderByDescending(s => s.Points).Take(3).ToList();
    }

    /// <inheritdoc />
    public IList<Score> GetAll()
    {
        using var context = new MastermindDbContext();
        return context.Scores.Include(t => t.Player).ToList();
    }

    /// <inheritdoc />
    public void Reset()
    {
        using var context = new MastermindDbContext();
        context.Database.ExecuteSqlRaw("DELETE FROM Scores");
    }
}