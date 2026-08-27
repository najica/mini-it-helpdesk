using System.Text.Json.Serialization;

namespace MiniItHelpdesk.Models
{
    public class User
    {
        public int Id  { get; set; }  
        public string Name { get; set; }
        public string Email { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }
        public enum UserRole
        {
            Employee, ITAgent, Admin, 
        }

        public UserRole Role { get; set; } = UserRole.Employee;
    }
}
