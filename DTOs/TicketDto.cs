using System;

public class TicketDto
{
	public int Id { get; set; }
	public string Title { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
    public string Status { get; set; }
	public TicketPriority Priority { get; set; }
	public enum TicketPriority
    {
        Low,
        Medium,
        High,
        Critical
    }
    public string Category { get; set; }
	public string CreatedAt { get; set; }
	public int CreatedByUserId { get; set; }
	public int? AssignedToUserId { get; set; }
}
