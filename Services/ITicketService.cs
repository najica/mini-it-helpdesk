using System;

public interface ITicketService
{
    Task<List<TicketDto>> GetAllAsync();
    Task<TicketDto?> GetByIdAsync(int id);
    Task<TicketDto?> CreateAsync(CreateTicketDto dto);
    Task<TicketDto?> UpdateAsync(int id, UpdateTicketDto dto);
    Task DeleteAsync(int id);
}
