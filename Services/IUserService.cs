using MiniItHelpdesk.DTOs;

namespace MiniItHelpdesk.Services
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllAsync();
        Task<List<UserDto>> GetAgentsAsync();
    }
}
