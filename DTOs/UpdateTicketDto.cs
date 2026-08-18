using System.ComponentModel.DataAnnotations;

public class UpdateTicketDto
{
    [Required]
    [StringLength(150)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(2000)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [EnumDataType(typeof(TicketPriority))]
    public TicketPriority Priority { get; set; }

    [Required]
    [EnumDataType(typeof(TicketCategory))]
    public TicketCategory Category { get; set; }
}