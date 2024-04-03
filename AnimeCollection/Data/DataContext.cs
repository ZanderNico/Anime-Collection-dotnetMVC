using AnimeCollection.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimeCollection.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Anime> Animes { get; set; }
        public DbSet<Genre> Genres { get; set; }

        public DbSet<AnimeGenre> AnimeGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnimeGenre>()
                .HasKey(ag => new { ag.AnimeId, ag.GenreId }); // Composite key for AnimeGenre

            modelBuilder.Entity<AnimeGenre>()
                .HasOne(ag => ag.Anime)
                .WithMany(a => a.AnimeGenres)
                .HasForeignKey(ag => ag.AnimeId); // Foreign key relationship between AnimeGenre and Anime

            modelBuilder.Entity<AnimeGenre>()
                .HasOne(ag => ag.Genre)
                .WithMany(g => g.AnimeGenres)
                .HasForeignKey(ag => ag.GenreId);
        }
    }
}
