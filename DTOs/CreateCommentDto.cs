using System;
using System.ComponentModel.DataAnnotations;

namespace MiniItHelpdesk.DTOs;

public class CreateCommentDto
{
    [Required, Range(1, int.MaxValue)]
    public int TicketId { get; set; }

    [Required, StringLength(1000)]
    public string Text { get; set; } = string.Empty;

    [Required, Range(1, int.MaxValue)]
    public int UserId { get; set; }

}
