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

        }
    }
}
