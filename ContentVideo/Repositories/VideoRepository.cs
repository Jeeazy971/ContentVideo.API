using ContentVideo.Data;
using ContentVideo.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ContentVideo.Repositories
{
    public class VideoRepository : IVideoRepository
    {
        private readonly ContentVideoDbContext _context;

        public VideoRepository(ContentVideoDbContext context)
        {
            _context = context;
        }

        public async Task<Video> CreateVideo(Video video)
        {
            _context.Videos.Add(video);
            await _context.SaveChangesAsync();
            return video;
        }

        public async Task<IEnumerable<Video>> GetAllVideos()
        {
            return await _context.Videos.ToListAsync();
        }

        public async Task<Video?> GetVideoById(Guid id)
        {
            return await _context.Videos
                                 .Include(v => v.Tags)
                                 .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<Video?> UpdateVideo(Guid id, Video video)
        {
            var existingVideo = await _context.Videos.FindAsync(id);
            if (existingVideo == null) return null;

            existingVideo.Title = video.Title;
            existingVideo.ShortDescription = video.ShortDescription;
            existingVideo.LongDescription = video.LongDescription;

            await _context.SaveChangesAsync();
            return existingVideo;
        }

        public async Task<bool> DeleteVideo(Guid id)
        {
            var video = await _context.Videos.FindAsync(id);
            if (video == null) return false;

            _context.Videos.Remove(video);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Video>> GetVideosByTagTitle(string tagTitle)
        {
            return await _context.Videos
                                 .Where(v => v.Tags.Any(t => t.Title == tagTitle))
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Video>> SearchVideosByTitleOrShortDescription(string searchText)
        {
            return await _context.Videos
                                 .Where(v => EF.Functions.Like(v.Title, $"%{searchText}%")
                                             || EF.Functions.Like(v.ShortDescription, $"%{searchText}%"))
                                 .ToListAsync();
        }

    }
}
