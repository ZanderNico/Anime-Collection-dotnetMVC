using AnimeCollection.Models;

namespace AnimeCollection.Interfaces
{
    public interface IAnimeRepository
    {
        Task<IEnumerable<Anime>> GetAnimes();
        Task<Anime> GetAnimeById(int id);
        Task<IEnumerable<Genre>> GetGenresForAnime(int animeId);
        bool Add(Anime anime);
        Task<bool> AddGenreToAnime(int animeId, int genreId);
        Task<bool> RemoveGenreFromAnime(int animeId, int genreId);
        Task<IEnumerable<Anime>> GetAnimesSearch(string searchString = null);
        bool Update(Anime anime);
        bool Delete(Anime anime);
        bool Save();
    }
}
