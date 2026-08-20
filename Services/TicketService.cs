using Microsoft.EntityFrameworkCore;
using MiniItHelpdesk.Data;
using MiniItHelpdesk.Models;
using System;
using SQLitePCL;
using System.Net.Sockets;
using MiniItHelpdesk.DTOs;
using MiniItHelpdesk.Enums;

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

    public async Task<TicketDto?> UpdateAsync(int id, UpdateTicketDto dto)
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

    public async Task<TicketDto?> ChangeStatusAsync(int id, ChangeStatusDto dto)
    {
        var ticket = await _context.Tickets.FindAsync(id);

        if (ticket is null)
            return null;

        if (ticket.Status == TicketStatus.Closed)
        {
            throw new InvalidOperationException(
                "A closed ticket cannot change status.");
        }

        ticket.Status = dto.NewStatus;
        await _context.SaveChangesAsync();

        return MapToDto(ticket);
    }
    
    public async Task<List<TicketDto>> SearchAsync(TicketStatus? status, TicketPriority? priority, TicketCategory? category, int? userId)
    {
        var tickets = await _context.Tickets
            .Where(t => (status == null || t.Status == status) &&
                        (priority == null || t.Priority == priority) &&
                        (category == null || t.Category == category) &&
                        (userId == null || t.CreatedByUserId == userId))
            .ToListAsync();
        return tickets.Select(MapToDto).ToList();
    }

    public Task<TicketDto?> AssignAsync(int id, AssignTicketDto dto)
    {
        throw new NotImplementedException();
    }
}

