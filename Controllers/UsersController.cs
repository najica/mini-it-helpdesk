using Microsoft.AspNetCore.Mvc;
using MiniItHelpdesk.DTOs;
using MiniItHelpdesk.Services;
using Microsoft.AspNetCore.Authorization;

namespace MiniItHelpdesk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService) => _userService = userService;

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("agents")]
        public async Task<ActionResult<List<UserDto>>> GetAgents()
        {
            var agents = await _userService.GetAgentsAsync();
            return Ok(agents);
        }
    }
}
