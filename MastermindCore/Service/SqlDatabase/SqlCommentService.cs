using MastermindCore.Entity;
using Microsoft.EntityFrameworkCore;

namespace MastermindCore.Service.SqlDatabase;

public class SqlCommentService : ICommentService
{
    /// <inheritdoc />
    public void Add(Comment comment)
    {
        using var context = new MastermindDbContext();
        context.Comments.Add(comment);
        context.SaveChanges();
    }

    /// <inheritdoc />
    public IList<Comment> GetAll()
    {
        using var context = new MastermindDbContext();
        return context.Comments.Include(t => t.Player).ToList();
    }

    /// <inheritdoc />
    public void Reset()
    {
        using var context = new MastermindDbContext();
        context.Database.ExecuteSqlRaw("DELETE FROM Comments");
    }
}