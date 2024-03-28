namespace ContentVideo.Models.Dtos
{
    using System.ComponentModel.DataAnnotations;

    public class CreateTagDTO
    {
        [Required(ErrorMessage = "Le titre du tag est obligatoire.")]
        public string Title { get; set; }
    }

}
