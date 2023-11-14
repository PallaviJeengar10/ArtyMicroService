using Arty.Dtos;
using Authentication.Services;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedModels;
using SharedModels.Models;

namespace Authentication.Controllers
{
    [Route("user")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPublishEndpoint _publish;

        public UserController(IUserService userService, IPublishEndpoint publish) 
        {
            _userService = userService;
            _publish = publish;
        }

        /// <summary>
        /// Authenticates a user with the provided login credentials and returns a JSON Web Token (JWT) if successful.
        /// </summary>
        /// <param name="userLogin">The user's login credentials (username and password).</param>
        /// <returns>
        ///   An HTTP response containing a JWT and the user's ID if the authentication is successful.
        ///   If authentication fails, returns a "User not found" message with a 404 status.
        /// </returns>
        [Route("login")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            var user = await _userService.Authenticate(userLogin);

            if (user != null)
            {
                var token = _userService.GenerateToken(user);
                var result = new
                {
                    token,
                    userId = user.UserId
                };
                return Ok(result);
            }

            return NotFound("User not found");
        }

        /// <summary>
        /// Registers a new user with the provided user profile information.
        /// </summary>
        /// <param name="userInfo">The user profile information to create a new user.</param>
        /// <returns>An HTTP response indicating the success of user registration.</returns>
        [Route("signUp")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] UserProfile userInfo)
        {
            int userId = await _userService.CreateUser(userInfo);

            if(userId > 0)
            {
                //Create Cart and Wishlist
                await _publish.PublishBatch<MessageModel>(new List<MessageModel>
                        {
                            new MessageModel{Event = MessageEvent.CreateCart, Value = userId.ToString()},
                            new MessageModel{Event = MessageEvent.CreateWishList, Value = userId.ToString()}
                        }) ;
            }
            

            return Ok(new { Message = $"User registered successfully! UserId: {userId}" });
        }

        [Route("getUserList")]
        [HttpGet]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetUserList()
        {
            var user = HttpContext.User;
            List<User> users = await _userService.GetUserList();
            return Ok(users);
        }

        [Route("getUser/{userId}")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserProfile(int userId)
        {
            var user = await _userService.GetUser(userId);
            if (user == null)
            {
                return NotFound("User Profile Not Found");
            }
            return Ok(user);
        }

        [Route("updateUser/{userId}")]
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateUser([FromRoute]int userId,[FromBody] UserSignup updatedUser)
        {
            if (userId == 0)
            {
                return await Task.FromResult<IActionResult>(NotFound());
            }
            if (await _userService.UpdateUser(userId, updatedUser))
            {
                return Ok("User Updated");
            }
            return NotFound();
        }

        [Route("deleteUser/{userId}")]
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteUser([FromRoute] int userId)
        {
            if (userId == 0)
            {
                return await Task.FromResult<IActionResult>(NotFound());
            }
            int deletedUserId = await _userService.DeleteUser(userId);
            if (deletedUserId == 0)
            {
                return NotFound();
            }
            return Ok("Removed User");
        }
    }
}
