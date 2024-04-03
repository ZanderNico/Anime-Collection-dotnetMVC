using System.ComponentModel.DataAnnotations;

namespace AnimeCollection.Models
{
	public class AnimeGenre
	{
		[Key]
		public int Id { get; set; }
		public int AnimeId { get; set; }
		public Anime Anime { get; set; }

		public int GenreId { get; set; }
		public Genre Genre { get; set; }
	}
}
