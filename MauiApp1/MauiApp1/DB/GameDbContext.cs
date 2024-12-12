

using MauiApp1.DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace MauiApp1.DB
{
    public class GameDbContext : DbContext
    {
        public DbSet<IncorrectAnswer> IncorrectAnswers { get; set; }
        public DbSet<Score> Scores { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=mydatabase.db");
        }
    }

}
