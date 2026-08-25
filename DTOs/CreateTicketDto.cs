using MiniItHelpdesk.Enums;
using System;
using System.ComponentModel.DataAnnotations;

public class CreateTicketDto
{
    [Required, StringLength(150, MinimumLength = 3)]
    public string Title { get; set; } = string.Empty;

    [Required, StringLength(2000, MinimumLength = 3)]
    public string Description { get; set; } = string.Empty;

    [EnumDataType(typeof(TicketPriority))]
    public TicketPriority? Priority { get; set; }

    [Required, EnumDataType(typeof(TicketCategory))]
    public TicketCategory TicketCategory { get; set; }

    [Required, Range(1, int.MaxValue)]
    public int CreatedByUserId { get; set; }
}
