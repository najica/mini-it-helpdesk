using MiniItHelpdesk.DTOs;
using MiniItHelpdesk.Enums;
using System;

public interface ITicketService
{
    Task<List<TicketDto>> GetAllAsync();
    Task<TicketDto?> GetByIdAsync(int id);
    Task<TicketDto?> CreateAsync(CreateTicketDto dto);
    Task<TicketDto?> UpdateAsync(int id, UpdateTicketDto dto);
    Task<bool> DeleteAsync(int id);
    Task<TicketDto?> ChangeStatusAsync(int id, ChangeStatusDto dto);
    Task<List<TicketDto>> SearchAsync(TicketStatus? status, TicketPriority? priority, TicketCategory? category, int? userId, string? search);
    Task<TicketDto?> AssignAsync(int id, AssignTicketDto dto);
}
