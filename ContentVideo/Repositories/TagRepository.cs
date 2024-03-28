using ContentVideo.Data;
using ContentVideo.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ContentVideo.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly ContentVideoDbContext _context;

        public TagRepository(ContentVideoDbContext context)
        {
            _context = context;
        }

        public async Task<Tag> CreateTag(Tag tag)
        {
            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();
            return tag;
        }

        public async Task<IEnumerable<Tag>> GetAllTags()
        {
            return await _context.Tags.ToListAsync();
        }

        public async Task<Tag?> GetTagById(Guid id)
        {
            return await _context.Tags.FindAsync(id);
        }

        public async Task<Tag?> UpdateTag(Guid id, Tag tag)
        {
            var existingTag = await _context.Tags.FindAsync(id);
            if (existingTag == null) return null;

            existingTag.Title = tag.Title;

            await _context.SaveChangesAsync();
            return existingTag;
        }

        public async Task<bool> DeleteTag(Guid id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag == null) return false;

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Tag>> SearchTagsByTitle(string title)
        {
            return await _context.Tags
                                 .Where(t => EF.Functions.Like(t.Title, $"%{title}%"))
                                 .ToListAsync();
        }

    }
}
