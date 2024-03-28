namespace ContentVideo.Models.Domain
{
    public class Tag
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public ICollection<Video> Videos { get; set; } = new List<Video>();
    }
}