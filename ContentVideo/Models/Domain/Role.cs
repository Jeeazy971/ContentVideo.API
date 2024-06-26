﻿using ContentVideo.Models.Domain;

public class Role
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; }
    public string Description { get; set; }
    public List<User> Users { get; set; } = new List<User>();
}
