using Microsoft.EntityFrameworkCore;
using MiniItHelpdesk.Data;
using MiniItHelpdesk.Models;
using MiniItHelpdesk.DTOs;

namespace MiniItHelpdesk.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context) => _context = context;

        public async Task<List<UserDto>> GetAllAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users.Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Role = u.Role
            }).ToList();
        }

        public async Task<List<UserDto>> GetAgentsAsync()
        {
            var users = await _context.Users.Where(u => u.Role.Equals(User.UserRole.ITAgent)).ToListAsync();
            return users.Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Role = u.Role
            }).ToList();
        }
    }
}
