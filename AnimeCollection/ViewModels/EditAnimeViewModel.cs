using AnimeCollection.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace AnimeCollection.ViewModels
{
    public class EditAnimeViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public IFormFile PosterImage { get; set; }

        [Required]
        public string Description { get; set; }

        public AnimeStatus Status { get; set; }

        public int Episodes { get; set; }

        public string Studio { get; set; }

        [Display(Name = "Date Aired")]
        [DataType(DataType.Date)]
        public DateTime DateAired { get; set; }
    }
}
