using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureApiWithJwt.Services.IServices;

namespace SecureApiWithJwt.Controllers
{
    [Route("api/intern")]
    [ApiController]
    [Authorize]
    public class InternController : Controller
    {
        private readonly IInternService _internService;

        public InternController(IInternService internService)
        {
            _internService = internService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllIntern()
        {
            var response = await _internService.GetInternsAsync();
            if (response.Code == 0)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInternById(int id)
        {
            var response = await _internService.GetInternByIdAsync(id);
            if (response.Code == 0)
            {
                return Ok(response);
            }
            return NotFound(response);
        }
    }
}
