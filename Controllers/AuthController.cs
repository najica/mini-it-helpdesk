using Microsoft.AspNetCore.Mvc;
using MiniItHelpdesk.DTOs;
using MiniItHelpdesk.Services;
using Microsoft.AspNetCore.Authorization;

namespace MiniItHelpdesk.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.LoginAsync(dto);

            if (result is null)
                return Unauthorized(new { message = "Pogrešan email ili lozinka." });

            return Ok(result);
        }
    }
}