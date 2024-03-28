using System.ComponentModel.DataAnnotations;

namespace ContentVideo.Models.Dtos
{
    public class VideoDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Title { get; set; }

        [Required]
        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        public List<string> Tags { get; set; } = new List<string>();
    }
}
