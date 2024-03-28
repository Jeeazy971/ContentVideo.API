using ContentVideo.Data;
using ContentVideo.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ContentVideo.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ContentVideoDbContext _context;

        public RoleRepository(ContentVideoDbContext context)
        {
            _context = context;
        }

        public async Task<Role> CreateRole(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role?> GetRoleById(Guid id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task<Role?> UpdateRole(Guid id, Role role)
        {
            var existingRole = await _context.Roles.FindAsync(id);
            if (existingRole == null) return null;

            existingRole.Title = role.Title;
            existingRole.Description = role.Description;

            await _context.SaveChangesAsync();
            return existingRole;
        }

        public async Task<bool> DeleteRole(Guid id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null) return false;

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Role?> GetRoleByTitle(string title)
        {
            return await _context.Roles
                                 .FirstOrDefaultAsync(r => r.Title.ToLower() == title.ToLower());
        }
    }
}
