using Microsoft.EntityFrameworkCore;
using MiniItHelpdesk.Models;

namespace MiniItHelpdesk.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<Ticket> Tickets { get; set; } 

    }
}
