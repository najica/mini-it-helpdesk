using System.ComponentModel.DataAnnotations;

namespace MiniItHelpdesk.DTOs
{
    public class AssignTicketDto
    {
        [Required, Range(1, int.MaxValue)]
        public int AssignedToUserId { get; set; }
    }
}
