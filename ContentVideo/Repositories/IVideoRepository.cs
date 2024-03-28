using ContentVideo.Models.Domain;

namespace ContentVideo.Repositories
{
    public interface IVideoRepository
    {
        Task<Video> CreateVideo(Video video);
        Task<IEnumerable<Video>> GetAllVideos();
        Task<Video?> GetVideoById(Guid id);
        Task<Video?> UpdateVideo(Guid id, Video video);
        Task<bool> DeleteVideo(Guid id);
        Task<IEnumerable<Video>> GetVideosByTagTitle(string tagTitle);
        Task<IEnumerable<Video>> SearchVideosByTitleOrShortDescription(string searchText);


    }
}
