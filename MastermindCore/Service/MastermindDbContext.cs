using MastermindCore.Entity;
using Microsoft.EntityFrameworkCore;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace MastermindCore.Service;

#nullable disable
public class MastermindDbContext: DbContext
{
    public DbSet<Score> Scores { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<Player> Players { get; set; }
 
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Mastermind;Trusted_Connection=True;");
}

#nullable enable