using MiniItHelpdesk.DTOs;

namespace MiniItHelpdesk.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> LoginAsync(LoginDto dto);
    }
}