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
}
