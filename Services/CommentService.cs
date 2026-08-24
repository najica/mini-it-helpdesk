using System;
using Microsoft.EntityFrameworkCore;
using MiniItHelpdesk.Data;
using MiniItHelpdesk.DTOs;
using MiniItHelpdesk.Models;

namespace MiniItHelpdesk.Services;

public class CommentService : ICommentService
{
    private readonly AppDbContext _context;

    public CommentService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<List<CommentDto>> GetAllAsync()

    {
        var comments = await _context.Comments.ToListAsync();
        return comments.Select(c => new CommentDto
        {
            Id = c.Id,
            CreatedAt = c.CreatedAt,
            TicketId = c.TicketId,
            UserId = c.UserId
        }).ToList();
    }

    public async Task<CommentDto?> GetByIdAsync(int id)
    {
        var comment = await _context.Comments.FindAsync(id);
        return comment is null ? null : new CommentDto
        {
            Id = comment.Id,
            CreatedAt = comment.CreatedAt,
            TicketId = comment.TicketId,
            UserId = comment.UserId
        };
    }

    public async Task<CommentDto?> CreateAsync(CreateCommentDto dto)
    {
        var comment = new Comment
        {
            TicketId = dto.TicketId,
            UserId = dto.UserId,
            CreatedAt = DateTime.UtcNow
        };
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();
        return new CommentDto
        {
            Id = comment.Id,
            CreatedAt = comment.CreatedAt,
            TicketId = comment.TicketId,
            UserId = comment.UserId
        };
    }
}
