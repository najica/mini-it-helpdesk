namespace MiniItHelpdesk.Models
{
    public class User
    {
        public int Id;
        public string Name;
        public string Email;
        public enum UserRole
        {
            Employee, ITAgent, Admin, 
        }

        public UserRole Role;
    }
}
