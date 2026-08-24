using Microsoft.AspNetCore.Mvc;
using MiniItHelpdesk.Enums;
using MiniItHelpdesk.DTOs;

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

        [HttpGet]
        public async Task<ActionResult<List<TicketDto>>> GetTickets([FromQuery] TicketStatus? status,
                                            [FromQuery] TicketPriority? priority,
                                            [FromQuery] TicketCategory? category,
                                            [FromQuery] int? user)
        {
            var tickets = await _ticketService.SearchAsync(status, priority, category, user);
            return Ok(tickets);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TicketDto>> GetById(int id)
        {
            var ticket = await _ticketService.GetByIdAsync(id);
            if (ticket is null)
                return NotFound();

            return Ok(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody] CreateTicketDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _ticketService.CreateAsync(dto);
            return StatusCode(201, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateTicketDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _ticketService.UpdateAsync(id, dto);
            if (updated is null)
                return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _ticketService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpPatch("{id}/status")]
        public async Task<ActionResult<TicketDto>> ChangeStatus(int id, [FromBody] ChangeStatusDto dto){
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updated = await _ticketService.ChangeStatusAsync(id, dto);

                if (updated is null)
                    return NotFound();

                return Ok(updated);
            }
            catch (InvalidOperationException exception)
            {
                return Conflict(new { message = exception.Message });
            }
        }

        [HttpGet("boom")]
        public IActionResult Boom() => throw new InvalidOperationException("namerna gre�ka");

        [HttpPatch("{id}/assign")]
        public async Task<IActionResult> Assign(int id, [FromBody] AssignTicketDto dto)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);    mrtav kod jer je provera default zbog [ApiController] atributa

            var assigned = await _ticketService.AssignAsync(id, dto);
            if (assigned is null)
                return NotFound();

            return Ok(assigned);
        }
    }
}
