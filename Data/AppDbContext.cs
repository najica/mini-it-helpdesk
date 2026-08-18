using Microsoft.EntityFrameworkCore;
using MiniItHelpdesk.Enums;
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

        public DbSet<User> Users { get; set; }

        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().Property(u => u.Name);
            modelBuilder.Entity<User>().Property(u => u.Email);
            modelBuilder.Entity<User>().Property(u => u.Role)
                .HasConversion<string>()
                .HasColumnType("TEXT");
            modelBuilder.Entity<Ticket>().HasKey(t => t.Id);
            modelBuilder.Entity<Ticket>().Property(t => t.Status)
                .HasConversion<string>()
                .HasColumnType("TEXT");
            modelBuilder.Entity<Ticket>().Property(t => t.Priority)
                .HasConversion<string>()
                .HasColumnType("TEXT");
            modelBuilder.Entity<Ticket>().Property(t => t.Category)
                .HasConversion<string>()
                .HasColumnType("TEXT");
            modelBuilder.Entity<Ticket>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(t => t.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Ticket>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(t => t.AssignedToUserId)
                .OnDelete(DeleteBehavior.SetNull);

            // Comment relationships
            // Comment → Ticket: Cascade (deleting ticket deletes its comments)
            modelBuilder.Entity<Comment>()
                .HasOne<Ticket>()
                .WithMany()
                .HasForeignKey(c => c.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            // Comment → User: Restrict (cannot delete user who has comments)
            modelBuilder.Entity<Comment>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed test users with fixed IDs for other students to test Ticket endpoints
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Employee", Email = "employee@test.com", Role = User.UserRole.Employee },
                new User { Id = 2, Name = "Direct Manager", Email = "direct.manager@test.com", Role = User.UserRole.ITAgent }
            );

            modelBuilder.Entity<Ticket>().HasData(
                new Ticket { Id = 1, Title = "Printer not working", Description = "The printer on the second floor is unresponsive, likely a driver issue.", Status = TicketStatus.Open, Priority = TicketPriority.Medium, Category = TicketCategory.Hardware, CreatedAt = new DateTime(2025, 1, 10, 9, 30, 0, DateTimeKind.Utc), CreatedByUserId = 1, AssignedToUserId = null },
                new Ticket { Id = 2, Title = "Cannot log into the system", Description = "Login returns 'Invalid credentials' error even though the password is correct.", Status = TicketStatus.InProgress, Priority = TicketPriority.High, Category = TicketCategory.Software, CreatedAt = new DateTime(2025, 1, 11, 11, 15, 0, DateTimeKind.Utc), CreatedByUserId = 1, AssignedToUserId = 2 },
                new Ticket { Id = 3, Title = "Slow internet connection", Description = "Internet has been extremely slow since this morning, affecting the whole team.", Status = TicketStatus.Resolved, Priority = TicketPriority.Low, Category = TicketCategory.Network, CreatedAt = new DateTime(2025, 1, 8, 8, 0, 0, DateTimeKind.Utc), CreatedByUserId = 2, AssignedToUserId = 2 },
                new Ticket { Id = 4, Title = "New software license needed", Description = "Adobe Photoshop license needed for a new employee.", Status = TicketStatus.Closed, Priority = null, Category = TicketCategory.Software, CreatedAt = new DateTime(2025, 1, 5, 14, 45, 0, DateTimeKind.Utc), CreatedByUserId = 1, AssignedToUserId = 2 }
            );
        }
    }
}
