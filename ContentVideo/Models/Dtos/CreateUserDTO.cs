using System.ComponentModel.DataAnnotations;

namespace ContentVideo.Models.Dtos
{
    public class CreateUserDTO
    {
        [Required(ErrorMessage = "Le nom d'utilisateur est requis.")]
        [MinLength(2, ErrorMessage = "Le nom doit être minium 2 caractères")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Le mot de passe est requis.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Le titre du rôle est requis.")]
        public string RoleTitle { get; set; }
    }
}
