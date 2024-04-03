using AnimeCollection.Data;
using AnimeCollection.Interfaces;
using AnimeCollection.Models;
using AnimeCollection.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AnimeCollection.Controllers
{
    public class AnimeController : Controller
    {
        private readonly IAnimeRepository _animeRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IPhotoService _photoService;

        public AnimeController(IAnimeRepository animeRepository, IGenreRepository genreRepository, IPhotoService photoService)
        {
            _animeRepository = animeRepository;
            _genreRepository = genreRepository;
            _photoService = photoService;
        }
       /* public async Task<IActionResult> Index()
        {
            IEnumerable<Anime> anime = await _animeRepository.GetAnimes();
            return View(anime);
        }  */

        public async Task<IActionResult> Detail(int id)
        {
            Anime anime = await _animeRepository.GetAnimeById(id);
            IEnumerable<Genre> genres = await _animeRepository.GetGenresForAnime(id);

            // Pass anime and genres to the view
            ViewData["Genres"] = genres;

            return View(anime);
        }

        public IActionResult Create()
        {
            ViewBag.Genres = _genreRepository.GetGenres().Result; // Assuming GetGenres() returns Task<IEnumerable<Genre>>
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAnimeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(viewModel.PosterImage);
                // Map properties from view model to Anime entity
                var anime = new Anime
                {
                    Title = viewModel.Title,
                    PosterImage = result.Url.ToString(),
                    Description = viewModel.Description,
                    Status = viewModel.Status,
                    Episodes = viewModel.Episodes,
                    Studio = viewModel.Studio,
                    DateAired = viewModel.DateAired
                };

                _animeRepository.Add(anime);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Handle upload failure
                ModelState.AddModelError("", "Failed to upload photo.");
            }
            return View(viewModel);

        }

        public async Task<IActionResult> AnimeGenre()
        {
            IEnumerable<Genre> genre = await _genreRepository.GetGenres();
            return View(genre);
        }

        [HttpGet]
        public async Task<IActionResult> AnimeGenre(int id)
        {
            var anime = await _animeRepository.GetAnimeById(id);
            ViewBag.Genres = await _genreRepository.GetGenres();
            return View("AnimeGenre", anime);
        }

        [HttpPost]
        public async Task<IActionResult> AnimeGenre(int animeId, int[] selectedGenres)
        {
            if (selectedGenres != null)
            {
                foreach (var genreId in selectedGenres)
                {
                    await _animeRepository.AddGenreToAnime(animeId, genreId);
                }
            }
            return RedirectToAction(nameof(Detail), new { id = animeId });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveGenresFromAnime(int animeId, int[] selectedGenres)
        {
            if (selectedGenres != null)
            {
                foreach (var genreId in selectedGenres)
                {
                    await _animeRepository.RemoveGenreFromAnime(animeId, genreId);
                }
            }
            return RedirectToAction(nameof(Detail), new { id = animeId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            // Retrieve the anime by its ID
            var anime = await _animeRepository.GetAnimeById(id);

            // If the anime exists, delete it
            if (anime != null)
            {
                // Delete the anime and its associated genres
                _animeRepository.Delete(anime);

                await _photoService.DeletePhotoAsync(anime.PosterImage);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Anime not found
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditAnimeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(viewModel.PosterImage);

                var anime = new Anime
                {
                    Id = viewModel.Id,
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    PosterImage = result.Url.ToString(),
                    Studio = viewModel.Studio,
                    Episodes = viewModel.Episodes,
                    DateAired = viewModel.DateAired,
                    Status = viewModel.Status
                };

                 _animeRepository.Update(anime);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("", "Failed to upload photo.");
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Anime anime = await _animeRepository.GetAnimeById(id);
            return View(anime);
        }

        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            IEnumerable<Anime> anime;

            if (!String.IsNullOrEmpty(searchString))
            {
                anime = await _animeRepository.GetAnimesSearch(searchString);
            }
            else
            {
                anime = await _animeRepository.GetAnimes();
            }

            return View(anime);
        }
    }
}
