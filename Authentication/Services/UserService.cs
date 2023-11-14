using Arty.Dtos;
using Authentication.DataRepositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SharedModels.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Arty.Helper;

namespace Authentication.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IConfiguration config, IMapper mapper)
        {
            _userRepository = userRepository;
            _config = config;
            _mapper = mapper;
        }

        public async Task<User?> Authenticate(UserLogin userLogin)
        {
            var currentUser = await _userRepository.GetUser(userLogin);

            if (currentUser != null)
            {
                return currentUser;
            }

            return null;
        }

        public string GenerateToken(User user)
        {
            var key = _config["Jwt:Key"];
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key), "Jwt:Key cannot be null or empty");
            }
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName)
            };

            var roleMapping = new Dictionary<int, string>
            {
                { 1, "User" },
                { 2, "Admin" }
            };

            if (user.RoleId != null)
            {
                roleMapping.TryGetValue((int)user.RoleId, out var roleName);
                if (roleName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Role, roleName));
                }
            }

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(15),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<int> CreateUser(UserProfile userInfo)
        {
            User user = _mapper.Map<User>(userInfo.User);
            var role = Convert.ToString(UserRole.User) ?? "User";
            user.RoleId = await _userRepository.GetRoleId(role);
            return await _userRepository.CreateUser(user);
        }

        public Task<List<User>> GetUserList()
        {
            return _userRepository.GetUserList();
        }

        public async Task<bool> UpdateUser(int id, UserSignup updatedUser)
        {
            var user = await _userRepository.GetUserById(id);
            if (user != null)
            {
                await _userRepository.UpdateUser(user, updatedUser);
                return true;
            }
            return false;
        }

        public async Task<int> DeleteUser(int userId)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user != null)
            {
                return await _userRepository.DeleteUser(user);
            }
            return 0;
        }

        public async Task<User?> GetUser(int userId)
        {
           return await _userRepository.GetUserById(userId);
        }
    }
}
