using System.Xml;

namespace ContentVideo.Models.Domain
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<User> Users { get; set; } = new List<User>();
    }
}