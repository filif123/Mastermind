using MastermindCore.Entity;
using Microsoft.EntityFrameworkCore;

namespace MastermindCore.Service.SqlDatabase;

public class SqlRatingService : IRatingService
{
    /// <inheritdoc />
    public bool Add(Rating rating)
    {
        using var context = new MastermindDbContext();
        var current = context.Ratings.FirstOrDefault(r => r.PlayerName == rating.PlayerName);
        if (current is null)
        {
            context.Ratings.Add(rating);
            context.SaveChanges();
            return true;
        }

        context.Attach(current);
        current.RatedAt = rating.RatedAt;
        current.Stars = rating.Stars;
        context.SaveChanges();
        return false;
    }

    /// <inheritdoc />
    public IList<Rating> GetAll()
    {
        using var context = new MastermindDbContext();
        return context.Ratings.Include(t => t.Player).ToList();
    }

    /// <inheritdoc />
    public Rating? GetByName(string name)
    {
        using var context = new MastermindDbContext();
        return context.Ratings.Include(t => t.Player).FirstOrDefault(r => r.Player != null && r.Player.Name == name);
    }

    /// <inheritdoc />
    public Rating? GetByPlayer(Player player)
    {
        using var context = new MastermindDbContext();
        return context.Ratings.Include(t => t.Player).FirstOrDefault(r => r.Player == player);
    }

    /// <inheritdoc />
    public double GetAvg()
    {
        using var context = new MastermindDbContext();
        if (!context.Ratings.Any())
            return 0;
        return context.Ratings.Average(r => r.Stars);
    }

    /// <inheritdoc />
    public void Reset()
    {
        using var context = new MastermindDbContext();
        context.Database.ExecuteSqlRaw("DELETE FROM Ratings");
    }
}