using System;

public class TicketService : ITicketService

{
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

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}

