using ContentVideo.Models.Domain;

namespace ContentVideo.Repositories
{
    public interface IRoleRepository
    {
        Task<Role> CreateRole(Role role);
        Task<IEnumerable<Role>> GetAllRoles();
        Task<Role?> GetRoleById(Guid id);
        Task<Role?> UpdateRole(Guid id, Role role);
        Task<bool> DeleteRole(Guid id);
        Task<Role?> GetRoleByTitle(string title);

    }
}
