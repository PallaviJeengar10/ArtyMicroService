using Arty.Dtos;
using Arty.Models;
using Microsoft.EntityFrameworkCore;
using SharedModels.Models;
using System.Data;

namespace Authentication.DataRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ArtyContext _context;

        public UserRepository(ArtyContext context)
        {
            _context = context;
        }
        public async Task<User?> GetUser(UserLogin userLogin)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(
                    o => o.Username.ToLower() == userLogin.Username.ToLower()
                    && o.Password == userLogin.Password);
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<int> CreateUser(User user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return user.UserId;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public async Task<List<User>> GetUserList()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task UpdateUser(User user, UserSignup updatedUser)
        {
            user.Username = updatedUser.Username ?? user.Username;
            user.Password = updatedUser.Password ?? user.Password;
            user.Email = updatedUser.Email ?? user.Email;
            user.FirstName = updatedUser.FirstName ?? user.FirstName;
            user.LastName = updatedUser.LastName ?? user.LastName;
            await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteUser(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user.UserId;
        }

        public async Task<User?> GetUserById(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<int> GetRoleId(string role)
        {
            return await _context.Roles
                .Where(r => r.UserRole == role)
                .Select(r => r.RoleId)
                .FirstOrDefaultAsync();
        }
    }
}
