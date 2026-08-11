using Microsoft.EntityFrameworkCore;

namespace MiniItHelpdesk.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }
    }
}
