using Microsoft.EntityFrameworkCore;

namespace ApiØvelse.Models
{
    public class GameInteractor : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = Game.db");
        }
        public DbSet<Game> GameInfo { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().Property(g => g.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Game>().HasData(
            new Game() { Id = 1, Title = "Me", Genre = "Horror", ReleaseYear = 1023 ,CreatedAt = DateTime.Now },
            new Game() { Id = 2, Title = "You", Genre = "Action", ReleaseYear = 1011, CreatedAt = DateTime.Now }
            );
        }
    }
}
