using System;

public class CreateTicketDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TicketPriority Priority { get; set; }
    public enum TicketPriority
    {
        Low,
        Medium,
        High,
        Critical
    }
    public string TicketCategory { get; set; }
    public int CreatedByUserId { get; set; }
}
