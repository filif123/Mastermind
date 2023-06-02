using MastermindCore.Entity;
using Microsoft.EntityFrameworkCore;

namespace MastermindCore.Service.SqlDatabase;

public class SqlPlayerService : IPlayerService
{
    /// <inheritdoc />
    public void Add(Player player)
    {
        using var context = new MastermindDbContext();
        context.Players.Add(player);
        context.SaveChanges();
    }

    /// <inheritdoc />
    public Player? Get(string name, string password)
    {
        using var context = new MastermindDbContext();
        return context.Players.Include(t => t.Comments).Include(t => t.Scores).Include(t => t.Rating)
            .FirstOrDefault(p => p.Name == name && p.PasswordHash == Encryptor.Md5Hash(password));
    }

    /// <inheritdoc />
    public bool Exists(string name)
    {
        using var context = new MastermindDbContext();
        return context.Players.Any(p => p.Name == name);
    }

    /// <inheritdoc />
    public IList<Player> GetAll()
    {
        using var context = new MastermindDbContext();
        return context.Players.Include(t => t.Comments).Include(t => t.Scores).Include(t => t.Rating).ToList();
    }

    /// <inheritdoc />
    public void Reset()
    {
        using var context = new MastermindDbContext();
        context.Database.ExecuteSqlRaw("DELETE FROM Players");
    }
}