namespace MiniItHelpdesk.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public enum TicketStatus
        {
            Open,
            InProgress,
            Resolved,
            Closed
        }

        public TicketStatus Status { get; set; } = TicketStatus.Open;

        public enum TicketPriority
        {
            Low,
            Medium,
            High,
            Critical
        }

        public TicketPriority? Priority { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //public User CreatedBy { get; set; } = null!;
    }
}
