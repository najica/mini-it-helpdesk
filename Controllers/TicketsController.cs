using Microsoft.AspNetCore.Mvc;
using MiniItHelpdesk.Enums;
using MiniItHelpdesk.DTOs;
using MiniItHelpdesk.Services;
using Microsoft.AspNetCore.Authorization;
using MiniItHelpdesk.Models;
using System.ComponentModel.DataAnnotations;

namespace MiniItHelpdesk.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly ICommentService _commentService;

        public TicketsController(ITicketService ticketService, ICommentService commentService)
        {
            _ticketService = ticketService;
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<TicketDto>>> GetTickets([FromQuery] TicketStatus? status,
                                            [FromQuery] TicketPriority? priority,
                                            [FromQuery] TicketCategory? category,
                                            [FromQuery] int? user,
                                            [FromQuery] string? search,
                                            [FromQuery] [Range(1, int.MaxValue)] int page = 1,
                                            [FromQuery] [Range(1, 60)] int pageSize = 10)
        {
            var tickets = await _ticketService.SearchAsync(status, priority, category, user, search, page, pageSize);
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

        [HttpGet("{ticketId}/comments")]
        public async Task<ActionResult<List<CommentDto>>> GetComments(int ticketId)
        {
            var ticket = await _ticketService.GetByIdAsync(ticketId);
            if (ticket is null)
                return NotFound();

            var comments = await _commentService.GetByTicketIdAsync(ticketId);
            return Ok(comments);
        }

        [HttpPost("{ticketId}/comments")]
        public async Task<IActionResult> CreateComment(int ticketId, [FromBody] CreateCommentDto dto)
        {
            var ticket = await _ticketService.GetByIdAsync(ticketId);
            if (ticket is null)
                return NotFound();

            dto.TicketId = ticketId;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _commentService.CreateAsync(dto);
            return StatusCode(201, created);
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
        [Authorize(Roles = "ITAgent,Admin")]
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
        [Authorize(Roles = "ITAgent,Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _ticketService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpPatch("{id}/status")]
        [Authorize(Roles = "ITAgent,Admin")]
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

        [HttpPatch("{id}/assign")]
        [Authorize(Roles = "ITAgent,Admin")]
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
