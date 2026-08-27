using MiniItHelpdesk.DTOs;
using MiniItHelpdesk.Models;

namespace MiniItHelpdesk.Services
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllAsync();
        Task<List<UserDto>> GetAgentsAsync();
        Task<User?> GetByEmailAsync(string email);
    }
}
