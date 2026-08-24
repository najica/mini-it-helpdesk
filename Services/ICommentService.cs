using MiniItHelpdesk.DTOs;

namespace MiniItHelpdesk.Services;

public interface ICommentService
{
    Task<List<CommentDto>> GetAllAsync();
    Task<CommentDto?> GetByIdAsync(int id);
    Task<CommentDto?> CreateAsync(CreateCommentDto dto);
}