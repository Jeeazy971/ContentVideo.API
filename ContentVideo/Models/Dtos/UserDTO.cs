﻿using System.ComponentModel.DataAnnotations;

namespace ContentVideo.Models.Dtos
{
    public class UserDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Username { get; set; }

        [Required]
        public string RoleTitle { get; set; }
    }

}
