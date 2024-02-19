using Microsoft.AspNetCore.Mvc;
using RAHSHU_BlogApi.Services.UserService;

namespace RAHSHU_BlogApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching users.");
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchUsers(string username)
        {
            try
            {
                var users = await _userService.Search(username);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while searching for users.");
            }
        }
    }
}