namespace ContentVideo.Models.Dtos
{
    using System.ComponentModel.DataAnnotations;

    public class UpdateUserDTO
    {
        [Required(ErrorMessage = "Le nom d'utilisateur est requis.")]
        [MinLength(2, ErrorMessage = "Le nom doit être minium 2 caractères")]
        public string Username { get; set; }
        public string Password { get; set; }
        public string RoleTitle { get; set; }
    }

}
