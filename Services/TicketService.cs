using Microsoft.EntityFrameworkCore;
using MiniItHelpdesk.Data;
using MiniItHelpdesk.Models;
using System;

public class TicketService : ITicketService
{
    private readonly AppDbContext _context;

    public TicketService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<TicketDto>> GetAllAsync()
    {
        var tickets = await _context.Tickets.ToListAsync();
        return tickets.Select(MapToDto).ToList();
    }

    public async Task<TicketDto?> GetByIdAsync(int id)
    {
        var ticket = await _context.Tickets.FindAsync(id);
        return ticket is null ? null : MapToDto(ticket);
    }

    public Task CreateAsync(CreateTicketDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<TicketDto?> UpdateAsync(int id, UpdateTicketDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var ticket = await _context.Tickets.FindAsync(id);
        if (ticket is null)
            return false;

        _context.Tickets.Remove(ticket);
        await _context.SaveChangesAsync();
        return true;
    }

    private static TicketDto MapToDto(Ticket ticket) => new TicketDto
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

