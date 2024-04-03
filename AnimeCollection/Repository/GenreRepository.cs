using AnimeCollection.Data;
using AnimeCollection.Interfaces;
using AnimeCollection.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimeCollection.Repository
{
    public class GenreRepository : IGenreRepository
    {
        private readonly DataContext _context;

        public GenreRepository(DataContext context)
        {
            _context = context;
        }

        public bool Add(Genre genre)
        {
            _context.Add(genre);
            return Save();
        }

        public bool Delete(Genre genre)
        {
            _context.Remove(genre);
            return Save();
        }

        public async Task<IEnumerable<Anime>> GetAnimesForGenre(int genreId)
        {
            var genre = await _context.Genres
                .Include(g => g.AnimeGenres)
                .ThenInclude(ag => ag.Anime)
                .FirstOrDefaultAsync(g => g.Id == genreId);

            if (genre == null)
                return null;

            return genre.AnimeGenres.Select(ag => ag.Anime);
        }

        public async Task<Genre> GetGenreById(int id)
        {
            return await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Genre>> GetGenres()
        {
            return await _context.Genres.ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
