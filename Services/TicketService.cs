using System;
using MiniItHelpdesk.Data;

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
}

