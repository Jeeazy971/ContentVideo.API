using System.ComponentModel.DataAnnotations;

namespace ContentVideo.Models.Dtos
{
    public class CreateVideoDTO
    {

        [Required]
        public string Title { get; set; }

        [Required]
        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        public List<string> TagTitles { get; set; } = new List<string>();
    }
}
