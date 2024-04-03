using AnimeCollection.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeCollection.Models
{
    public class Anime
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string PosterImage { get; set; }
        [Required]
        public string Description { get; set; }
        public AnimeStatus Status { get; set; }
        public int Episodes { get; set; }
        public string Studio { get; set; }
        public DateTime DateAired { get; set; }

        [ForeignKey("GenreId")]
        public ICollection<AnimeGenre> AnimeGenres { get; set; }

    }
}
