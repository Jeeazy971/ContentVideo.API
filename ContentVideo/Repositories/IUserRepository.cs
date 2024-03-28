using ContentVideo.Models.Domain;

namespace ContentVideo.Repositories
{
    public interface IUserRepository
    {
        Task<User> CreateUser(User user);
        Task<User?> UpdateUser(Guid id, User user);
        Task<User?> DeleteUser(Guid id);
        Task<User?> GetUserById(Guid id);
        Task<bool> UpdateUser(User user);
        Task<List<User>> GetAllUsers();
        Task<User?> Authenticate(string username, string password);


        Task<IEnumerable<User>> SearchUsersByUsername(string username);
        Task<IEnumerable<User>> GetUsersByRoleId(Guid roleId);

    }
}
