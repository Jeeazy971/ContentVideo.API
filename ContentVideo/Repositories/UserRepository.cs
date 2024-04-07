using ContentVideo.Data;
using ContentVideo.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ContentVideo.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ContentVideoDbContext _context;

        public UserRepository(ContentVideoDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> UpdateUser(Guid id, User userUpdate)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            user.Username = userUpdate.Username;
            user.Password = userUpdate.Password;
            user.RoleId = userUpdate.RoleId;
            
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<IEnumerable<User>> SearchUsersByUsername(string username)
        {
            return await _context.Users
                                 .Where(u => EF.Functions.Like(u.Username, $"%{username}%"))
                                 .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUsersByRoleId(Guid roleId)
        {
            return await _context.Users
                                 .Where(u => u.RoleId == roleId)
                                 .ToListAsync();
        }

        public async Task<User?> GetUserById(Guid id)
        {
            return await _context.Users
                                 .Include(u => u.Role)
                                 .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> UpdateUser(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.Id);
            if (existingUser == null) return false;

            existingUser.Username = user.Username;
            existingUser.RoleId = user.RoleId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> FindByUsername(string username)
        {
            return await _context.Users
                                 .Include(u => u.Role)
                                 .SingleOrDefaultAsync(u => u.Username == username);
        }

        public async Task<IEnumerable<User>> GetAllUsersWithRoles()
        {
            return await _context.Users.Include(u => u.Role).ToListAsync();
        }

        public async Task<User?> GetUserByIdWithRole(Guid id)
        {
            return await _context.Users
                                 .Include(u => u.Role)
                                 .SingleOrDefaultAsync(u => u.Id == id);
        }



    }
}
