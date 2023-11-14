using Arty.Dtos;
using SharedModels.Models;

namespace Authentication.DataRepositories
{
    public interface IUserRepository
    {
        public Task<User?> GetUser(UserLogin userLogin);
        public Task<int> CreateUser(User user);
        public Task<List<User>> GetUserList();
        public Task UpdateUser(User user, UserSignup updatedUser);
        public Task<int> DeleteUser(User user);
        public Task<User?> GetUserById(int userId);
        public Task<int> GetRoleId(string roleId);
    }
}
