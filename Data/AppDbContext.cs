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

        public DbSet<User> Users { get; set; }

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

            // Seed test users with fixed IDs for other students to test Ticket endpoints
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Employee", Email = "employee@test.com", Role = User.UserRole.Employee },
                new User { Id = 2, Name = "Direct Manager", Email = "direct.manager@test.com", Role = User.UserRole.ITAgent }
            );
        }
    }
}
