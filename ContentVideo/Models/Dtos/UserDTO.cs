using System.ComponentModel.DataAnnotations;

namespace ContentVideo.Models.Dtos
{
    public class UserDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string RoleTitle { get; set; }
    }

}
