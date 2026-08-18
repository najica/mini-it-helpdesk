using Microsoft.AspNetCore.Mvc;

namespace MiniItHelpdesk.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody] CreateTicketDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _ticketService.CreateAsync(dto);
            return StatusCode(201, created);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _ticketService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
