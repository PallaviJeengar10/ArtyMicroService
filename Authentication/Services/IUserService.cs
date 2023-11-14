using Arty.Dtos;
using SharedModels.Models;

namespace Authentication.Services
{
    public interface IUserService
    {
        public Task<User?> Authenticate(UserLogin userLogin);
        public string GenerateToken(User user);
        public Task<int> CreateUser(UserProfile user);
        public Task<List<User>> GetUserList();
        public Task<bool> UpdateUser(int id, UserSignup user);
        public Task<int> DeleteUser(int userId);
        public Task<User?> GetUser(int userId);
    }
}
