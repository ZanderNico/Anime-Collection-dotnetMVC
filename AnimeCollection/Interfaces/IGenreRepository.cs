using AnimeCollection.Models;

namespace AnimeCollection.Interfaces
{
    public interface IGenreRepository
    {
        Task<IEnumerable<Genre>> GetGenres();
        Task<Genre> GetGenreById(int id);
        Task<IEnumerable<Anime>> GetAnimesForGenre(int genreId);
        bool Add(Genre genre);
        bool Delete(Genre genre);
        bool Save();
    }
}
