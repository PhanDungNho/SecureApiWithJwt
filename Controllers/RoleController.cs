using Microsoft.AspNetCore.Mvc;
using SecureApiWithJwt.DTOs.Requests;
using SecureApiWithJwt.Services.IServices;

namespace SecureApiWithJwt.Controllers
{
    [Route("api/role")]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var response = await _roleService.GetRolesAsync();
            if (response.Code == 0)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _roleService.GetByIdAsync(id);
            if (response.Code == 0)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoleRequest roleRequest)
        {
            var response = await _roleService.CreateAsync(roleRequest);
            if (response.Code == 0)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RoleRequest roleRequest)
        {
            var response = await _roleService.UpdateAsync(id, roleRequest);
            if (response.Code == 0)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpPut("{id}/toggle-active")]
        public async Task<IActionResult> ToggleActive(int id)
        {
            var response = await _roleService.DeleteAsync(id);
            if (response.Code == 0)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

    }
}
