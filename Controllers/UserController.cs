using Microsoft.AspNetCore.Mvc;
using SecureApiWithJwt.Services.IServices;
using SecureApiWithJwt.DTOs.Requests;
using System.Threading.Tasks;

namespace SecureApiWithJwt.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var response = await _userService.GetUsersAsync(); 
            if (response.Code == 0)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var response = await _userService.GetUserByIdAsync(id); 
            if (response.Code == 0)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserRequest userRequest)
        {
            var response = await _userService.CreateUserAsync(userRequest); 
            if (response.Code == 0)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserRequest userRequest)
        {
            var response = await _userService.UpdateUserAsync(id, userRequest);
            if (response.Code == 0)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = await _userService.DeleteUserAsync(id);
            if (response.Code == 0)
            {
                return Ok(response);
            }
            return NotFound(response);
        }
    }
}
