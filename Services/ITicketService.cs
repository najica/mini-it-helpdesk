using System;

public interface ITicketService
{
    Task<List<TicketDto>> GetAllAsync();
    Task<TicketDto?> GetByIdAsync(int id);
    Task CreateAsync(CreateTicketDto dto);
    Task<TicketDto?> UpdateAsync(int id, UpdateTicketDto dto);
    Task<bool> DeleteAsync(int id);
}
