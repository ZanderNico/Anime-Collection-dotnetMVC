using AnimeCollection.Interfaces;
using AnimeCollection.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnimeCollection.Controllers
{
    public class GenreController : Controller
    {
		private readonly IGenreRepository _genreRepository;

		public GenreController(IGenreRepository genreRepository)
        {
			_genreRepository = genreRepository;
		}
		public async Task<IActionResult> Index()
		{
			var genres = await _genreRepository.GetGenres();
			return View(genres);
		}

		[HttpPost]
		public IActionResult Add(string name)
		{
			if (ModelState.IsValid)
			{
				var genre = new Genre { Name = name };
				_genreRepository.Add(genre);
				return RedirectToAction(nameof(Index));
			}
			return View();
		}

		// POST: Genre/Delete
		[HttpPost]
		public IActionResult Delete(int id)
		{
			var genre = _genreRepository.GetGenreById(id).Result;
			if (genre == null)
			{
				return NotFound();
			}

			_genreRepository.Delete(genre);
			return RedirectToAction(nameof(Index));
		}
	}
}
