using MiniItHelpdesk.Models;

namespace MiniItHelpdesk.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public User.UserRole Role { get; set; }
    }
}
