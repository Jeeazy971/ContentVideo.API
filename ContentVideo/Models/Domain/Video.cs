namespace ContentVideo.Models.Domain
{
    public class Video
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}
