using System.ComponentModel.DataAnnotations;

namespace ContentVideo.Models.Dtos
{
    public class TagDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Title { get; set; }
    }
}
