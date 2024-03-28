﻿using System.Xml;

namespace ContentVideo.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public Guid RoleId { get; set; }
        public Role Role { get; set; }

    }
}