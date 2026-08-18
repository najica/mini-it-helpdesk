using System;

public class TicketService : ITicketService

{
    public Task<List<TicketDto>> GetAllAsync()
    
    public Task<TicketDto?> GetByIdAsync(int id)
  
    public Task CreateAsync(CreateTicketDto dto)
  
    public Task<TicketDto?> UpdateAsync(int id, UpdateTicketDto dto)
    {
        var ticket = await _context.Tickets.FindAsync(id);
        if (ticket is null)
            return null;

        ticket.Title = dto.Title;
        ticket.Description = dto.Description;
        ticket.Priority = dto.Priority;
        ticket.Category = dto.Category;

        await _context.SaveChangesAsync();

        return new TicketDto
        {
            Id = ticket.Id,
            Title = ticket.Title,
            Description = ticket.Description,
            Status = ticket.Status,
            Priority = ticket.Priority,
            Category = ticket.Category,
            CreatedAt = ticket.CreatedAt,
            CreatedByUserId = ticket.CreatedByUserId,
            AssignedToUserId = ticket.AssignedToUserId
        };
    }

    public Task DeleteAsync(int id)
   
}

