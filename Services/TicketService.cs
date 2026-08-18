using MiniItHelpdesk.Data;
using MiniItHelpdesk.Models;
using SQLitePCL;
using System;
using System.Net.Sockets;

public class TicketService : ITicketService

{
    private readonly AppDbContext _context;

    public TicketService(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<TicketDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
    public Task<TicketDto?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
    public async Task<TicketDto?> CreateAsync(CreateTicketDto dto)
    {
        var Ticket = new Ticket
        {
            Title = dto.Title,
            Description = dto.Description,
            Priority = dto.Priority,
            Category = dto.TicketCategory,
            CreatedByUserId = dto.CreatedByUserId,
            AssignedToUserId = null,
            CreatedAt = DateTime.UtcNow
        };

        _context.Tickets.Add(Ticket);
        await _context.SaveChangesAsync();

        return new TicketDto
        {
            Id = Ticket.Id,
            Title = Ticket.Title,
            Description = Ticket.Description,
            Priority = Ticket.Priority,
            Category = Ticket.Category,
            CreatedByUserId = Ticket.CreatedByUserId,
            AssignedToUserId = Ticket.AssignedToUserId,
            CreatedAt = Ticket.CreatedAt,
            Status = Ticket.Status
        };
    }
    public Task<TicketDto?> UpdateAsync(int id, UpdateTicketDto dto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}

