using ContentVideo.Models.Domain;

namespace ContentVideo.Repositories
{
    public interface ITagRepository
    {
        Task<Tag> CreateTag(Tag tag);
        Task<IEnumerable<Tag>> GetAllTags();
        Task<Tag?> GetTagById(Guid id);
        Task<Tag?> UpdateTag(Guid id, Tag tag);
        Task<bool> DeleteTag(Guid id);
        Task<IEnumerable<Tag>> SearchTagsByTitle(string title);

    }
}