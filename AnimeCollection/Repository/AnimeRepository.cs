using AnimeCollection.Data;
using AnimeCollection.Interfaces;
using AnimeCollection.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimeCollection.Repository
{
    public class AnimeRepository : IAnimeRepository
    {
        private readonly DataContext _context;

        public AnimeRepository(DataContext context)
        {
            _context = context;
        }
        public bool Add(Anime anime)
        {
            _context.Add(anime);
            return Save();
        }

        public bool Delete(Anime anime)
        {
            // Remove associated genres first
            var animeGenres = _context.AnimeGenres.Where(ag => ag.AnimeId == anime.Id);
            _context.AnimeGenres.RemoveRange(animeGenres);

            // Remove the anime itself
            _context.Remove(anime);

            // Save changes to the database
            return Save();
        }

        public async Task<Anime> GetAnimeById(int id)
        {
            return await _context.Animes
                .Include(a => a.AnimeGenres)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Anime>> GetAnimes()
        {
            return await _context.Animes.ToListAsync();
        }

        public async Task<IEnumerable<Genre>> GetGenresForAnime(int animeId)
        {
            var anime = await _context.Animes
                .Include(a => a.AnimeGenres)
                .ThenInclude(ag => ag.Genre)
                .FirstOrDefaultAsync(a => a.Id == animeId);

            if (anime == null)
                return null;

            return anime.AnimeGenres.Select(ag => ag.Genre);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<bool> AddGenreToAnime(int animeId, int genreId)
        {
            var anime = await _context.Animes.FindAsync(animeId);
            var genre = await _context.Genres.FindAsync(genreId);

            if (anime == null || genre == null)
                return false;

            // Check if AnimeGenres is null and initialize it if necessary
            if (anime.AnimeGenres == null)
                anime.AnimeGenres = new List<AnimeGenre>();

            // Add new AnimeGenre to AnimeGenres collection
            anime.AnimeGenres.Add(new AnimeGenre { AnimeId = animeId, GenreId = genreId });

            return Save();
        }

        public async Task<bool> RemoveGenreFromAnime(int animeId, int genreId)
        {
            var animeGenre = await _context.AnimeGenres.FirstOrDefaultAsync(ag => ag.AnimeId == animeId && ag.GenreId == genreId);

            if (animeGenre == null)
                return false;

            _context.AnimeGenres.Remove(animeGenre);
            return Save();
        }

        public bool Update(Anime anime)
        {
            _context.Update(anime);
            return Save();
        }

        public async Task<IEnumerable<Anime>> GetAnimesSearch(string searchString)
        {
            var query = _context.Animes.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(a => a.Title.Contains(searchString));
            }

            return await query.ToListAsync();
        }
    }
}
