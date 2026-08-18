namespace MiniItHelpdesk.Models;

using MiniItHelpdesk.Enums;

public class Ticket
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public TicketStatus Status { get; set; } = TicketStatus.Open;

    public TicketPriority? Priority { get; set; }

    public TicketCategory Category { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int CreatedByUserId { get; set; }
    public int? AssignedToUserId { get; set; }
}
