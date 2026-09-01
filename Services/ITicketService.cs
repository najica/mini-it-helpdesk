using MiniItHelpdesk.DTOs;
using MiniItHelpdesk.Enums;
using MiniItHelpdesk.Models;
using System;

public interface ITicketService
{
    Task<List<TicketDto>> GetAllAsync();
    Task<TicketDto?> GetByIdAsync(int id);
    Task<TicketDto?> CreateAsync(CreateTicketDto dto);
    Task<TicketDto?> UpdateAsync(int id, UpdateTicketDto dto);
    Task<bool> DeleteAsync(int id);
    Task<TicketDto?> ChangeStatusAsync(int id, ChangeStatusDto dto);
    Task<PagedResult<TicketDto>> SearchAsync(TicketStatus? status, TicketPriority? priority, TicketCategory? category, int? userId, int page = 1, int pageSize = 10);
    Task<TicketDto?> AssignAsync(int id, AssignTicketDto dto);
}
