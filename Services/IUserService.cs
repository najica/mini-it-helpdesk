using MiniItHelpdesk.DTOs;

namespace MiniItHelpdesk.Services
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllAsync();
    }
}
