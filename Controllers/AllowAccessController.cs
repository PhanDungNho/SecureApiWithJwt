using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureApiWithJwt.DTOs.Requests;
using SecureApiWithJwt.Services.IServices;

namespace SecureApiWithJwt.Controllers
{
    [Route("api/allowaccess")]
    [ApiController]
    public class AllowAccessController : Controller
    {
        private readonly IAllowAccessService _allowAccessService;

        public AllowAccessController(IAllowAccessService allowAccessService)
        {
            _allowAccessService = allowAccessService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllowAccessesAsync()
        {
            var response = await _allowAccessService.GetAllowAccessesAsync();
            if (response.Code == 0)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var response = await _allowAccessService.GetByIdAsync(id);
            if (response.Code == 0)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] AllowAccessRequest allowAccessRequest)
        {
            var response = await _allowAccessService.CreateAsync(allowAccessRequest);
            if (response.Code == 0)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] AllowAccessRequest allowAccessRequest)
        {
            var response = await _allowAccessService.UpdateAsync(id, allowAccessRequest);
            if (response.Code == 0)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await _allowAccessService.DeleteAsync(id);
            if (response.Code == 0)
            {
                return Ok(response);
            }
            return NotFound(response);
        }
    }
}
