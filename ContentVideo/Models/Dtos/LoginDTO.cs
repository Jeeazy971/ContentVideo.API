using System.ComponentModel.DataAnnotations;

namespace ContentVideo.Models.Dtos
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Le nom d'utilisateur est requis")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Le mot de passe est requis")]
        public string Password { get; set; }
    }

}
