using MiniItHelpdesk.Models;
using System;

using MiniItHelpdesk.Enums;

public class TicketDto
{
	public int Id { get; set; }
	public string Title { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
    public TicketStatus Status { get; set; }
	public TicketPriority? Priority { get; set; }
    public TicketCategory Category { get; set; }
	public DateTime CreatedAt { get; set; }
	public int CreatedByUserId { get; set; }
	public int? AssignedToUserId { get; set; }

    private static TicketDto MapToDto(Ticket ticket) => new TicketDto
    {
        Id = ticket.Id,
        Title = ticket.Title,
        Description = ticket.Description,
        Status = ticket.Status,
        Priority = ticket.Priority,
        Category = ticket.Category,
        CreatedAt = ticket.CreatedAt,
        CreatedByUserId = ticket.CreatedByUserId,
        AssignedToUserId = ticket.AssignedToUserId
    };
}
