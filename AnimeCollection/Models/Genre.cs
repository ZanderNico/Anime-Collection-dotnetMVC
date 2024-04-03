using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeCollection.Models
{
	public class Genre
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[ForeignKey("AnimeId")]
		public ICollection<AnimeGenre> AnimeGenres { get; set; }
	}
}
