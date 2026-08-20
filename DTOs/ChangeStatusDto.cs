using MiniItHelpdesk.Enums;
using System.ComponentModel.DataAnnotations;

namespace MiniItHelpdesk.DTOs
{
    public class ChangeStatusDto
    {
        [Required, EnumDataType(typeof(TicketStatus))]
        public TicketStatus NewStatus { get; set; }
    }
}//
