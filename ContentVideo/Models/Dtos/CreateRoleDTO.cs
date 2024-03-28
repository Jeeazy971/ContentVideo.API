using System.ComponentModel.DataAnnotations;

namespace ContentVideo.Models.Dtos
{
    public class CreateRoleDTO
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
