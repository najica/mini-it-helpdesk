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
            modelBuilder.Entity<User>()
                .Property(u => u.PasswordHash)
                .HasMaxLength(255)
                .IsRequired();
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

           
            modelBuilder.Entity<Comment>()
                .HasOne<Ticket>()
                .WithMany()
                .HasForeignKey(c => c.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Employee", Email = "employee@test.com", Role = User.UserRole.Employee, PasswordHash = "$2a$11$LaHBE4wxxt2gjQUIVdnODu.Q6XfErT/61N/G/hW.RqV.RuoskmGS." },
                new User { Id = 2, Name = "Direct Manager", Email = "direct.manager@test.com", Role = User.UserRole.ITAgent, PasswordHash = "$2a$11$LaHBE4wxxt2gjQUIVdnODu.Q6XfErT/61N/G/hW.RqV.RuoskmGS." },
                new User { Id = 3, Name = "Ana Petrović", Email = "ana.petrovic@test.com", Role = User.UserRole.Employee, PasswordHash = "$2a$11$LaHBE4wxxt2gjQUIVdnODu.Q6XfErT/61N/G/hW.RqV.RuoskmGS." },
                new User { Id = 4, Name = "Marko Jovanović", Email = "marko.jovanovic@test.com", Role = User.UserRole.Employee, PasswordHash = "$2a$11$LaHBE4wxxt2gjQUIVdnODu.Q6XfErT/61N/G/hW.RqV.RuoskmGS." },
                new User { Id = 5, Name = "Jelena Nikolić", Email = "jelena.nikolic@test.com", Role = User.UserRole.ITAgent, PasswordHash = "$2a$11$LaHBE4wxxt2gjQUIVdnODu.Q6XfErT/61N/G/hW.RqV.RuoskmGS." },
                new User { Id = 6, Name = "Stefan Ilić", Email = "stefan.ilic@test.com", Role = User.UserRole.ITAgent, PasswordHash = "$2a$11$LaHBE4wxxt2gjQUIVdnODu.Q6XfErT/61N/G/hW.RqV.RuoskmGS." },
                new User { Id = 7, Name = "Milica Stanković", Email = "milica.stankovic@test.com", Role = User.UserRole.Employee, PasswordHash = "$2a$11$LaHBE4wxxt2gjQUIVdnODu.Q6XfErT/61N/G/hW.RqV.RuoskmGS." },
                new User { Id = 8, Name = "Admin User", Email = "admin@test.com", Role = User.UserRole.Admin, PasswordHash = "$2a$11$LaHBE4wxxt2gjQUIVdnODu.Q6XfErT/61N/G/hW.RqV.RuoskmGS." }
            );

            modelBuilder.Entity<Ticket>().HasData(
                new Ticket { Id = 1, Title = "Printer not working", Description = "The printer on the second floor is unresponsive, likely a driver issue.", Status = TicketStatus.Open, Priority = TicketPriority.Medium, Category = TicketCategory.Hardware, CreatedAt = new DateTime(2025, 1, 10, 9, 30, 0, DateTimeKind.Utc), CreatedByUserId = 1, AssignedToUserId = null },
                new Ticket { Id = 2, Title = "Cannot log into the system", Description = "Login returns 'Invalid credentials' error even though the password is correct.", Status = TicketStatus.InProgress, Priority = TicketPriority.High, Category = TicketCategory.Software, CreatedAt = new DateTime(2025, 1, 11, 11, 15, 0, DateTimeKind.Utc), CreatedByUserId = 1, AssignedToUserId = 2 },
                new Ticket { Id = 3, Title = "Slow internet connection", Description = "Internet has been extremely slow since this morning, affecting the whole team.", Status = TicketStatus.Resolved, Priority = TicketPriority.Low, Category = TicketCategory.Network, CreatedAt = new DateTime(2025, 1, 8, 8, 0, 0, DateTimeKind.Utc), CreatedByUserId = 2, AssignedToUserId = 2 },
                new Ticket { Id = 4, Title = "New software license needed", Description = "Adobe Photoshop license needed for a new employee.", Status = TicketStatus.Closed, Priority = null, Category = TicketCategory.Software, CreatedAt = new DateTime(2025, 1, 5, 14, 45, 0, DateTimeKind.Utc), CreatedByUserId = 1, AssignedToUserId = 2 },
                new Ticket { Id = 5, Title = "Monitor flickering", Description = "External monitor flickers randomly, possibly a cable or driver issue.", Status = TicketStatus.Open, Priority = TicketPriority.Low, Category = TicketCategory.Hardware, CreatedAt = new DateTime(2025, 1, 12, 9, 0, 0, DateTimeKind.Utc), CreatedByUserId = 3, AssignedToUserId = null },
                new Ticket { Id = 6, Title = "VPN connection drops", Description = "VPN disconnects every few minutes when working from home.", Status = TicketStatus.InProgress, Priority = TicketPriority.High, Category = TicketCategory.Network, CreatedAt = new DateTime(2025, 1, 13, 10, 20, 0, DateTimeKind.Utc), CreatedByUserId = 4, AssignedToUserId = 5 },
                new Ticket { Id = 7, Title = "Account locked out", Description = "Account got locked after several failed login attempts, needs unlocking.", Status = TicketStatus.Resolved, Priority = TicketPriority.Medium, Category = TicketCategory.Account, CreatedAt = new DateTime(2025, 1, 14, 13, 5, 0, DateTimeKind.Utc), CreatedByUserId = 7, AssignedToUserId = 6 },
                new Ticket { Id = 8, Title = "New laptop request", Description = "Current laptop is too slow for daily tasks, requesting a replacement.", Status = TicketStatus.Open, Priority = TicketPriority.Medium, Category = TicketCategory.Hardware, CreatedAt = new DateTime(2025, 1, 15, 8, 45, 0, DateTimeKind.Utc), CreatedByUserId = 3, AssignedToUserId = null },
                new Ticket { Id = 9, Title = "Email sync issues", Description = "Emails are not syncing on mobile device, only on desktop.", Status = TicketStatus.Closed, Priority = TicketPriority.Low, Category = TicketCategory.Software, CreatedAt = new DateTime(2025, 1, 16, 15, 30, 0, DateTimeKind.Utc), CreatedByUserId = 4, AssignedToUserId = 5 },
                new Ticket { Id = 10, Title = "Keyboard keys not responding", Description = "Several keys on the keyboard stopped working after a spill.", Status = TicketStatus.Open, Priority = TicketPriority.Low, Category = TicketCategory.Hardware, CreatedAt = new DateTime(2025, 1, 17, 9, 10, 0, DateTimeKind.Utc), CreatedByUserId = 7, AssignedToUserId = null },
                new Ticket { Id = 11, Title = "Cannot access shared drive", Description = "Shared network drive is not showing up after the latest update.", Status = TicketStatus.InProgress, Priority = TicketPriority.High, Category = TicketCategory.Network, CreatedAt = new DateTime(2025, 1, 18, 10, 40, 0, DateTimeKind.Utc), CreatedByUserId = 3, AssignedToUserId = 6 },
                new Ticket { Id = 12, Title = "Excel crashes on large files", Description = "Excel crashes consistently when opening files larger than 20MB.", Status = TicketStatus.Open, Priority = TicketPriority.Medium, Category = TicketCategory.Software, CreatedAt = new DateTime(2025, 1, 19, 11, 25, 0, DateTimeKind.Utc), CreatedByUserId = 4, AssignedToUserId = null },
                new Ticket { Id = 13, Title = "Request access to finance folder", Description = "New team member needs read access to the finance shared folder.", Status = TicketStatus.Resolved, Priority = TicketPriority.Low, Category = TicketCategory.Account, CreatedAt = new DateTime(2025, 1, 20, 8, 15, 0, DateTimeKind.Utc), CreatedByUserId = 7, AssignedToUserId = 5 },
                new Ticket { Id = 14, Title = "Second monitor not detected", Description = "Second monitor is not detected after docking station firmware update.", Status = TicketStatus.Closed, Priority = TicketPriority.Medium, Category = TicketCategory.Hardware, CreatedAt = new DateTime(2025, 1, 21, 14, 0, 0, DateTimeKind.Utc), CreatedByUserId = 3, AssignedToUserId = 6 },
                new Ticket { Id = 15, Title = "Wi-Fi keeps disconnecting", Description = "Laptop drops the office Wi-Fi connection every 10-15 minutes.", Status = TicketStatus.Open, Priority = TicketPriority.Medium, Category = TicketCategory.Network, CreatedAt = new DateTime(2025, 1, 22, 8, 30, 0, DateTimeKind.Utc), CreatedByUserId = 4, AssignedToUserId = null },
                new Ticket { Id = 16, Title = "Cannot install Slack update", Description = "Slack update fails with a permissions error every time.", Status = TicketStatus.InProgress, Priority = TicketPriority.Low, Category = TicketCategory.Software, CreatedAt = new DateTime(2025, 1, 22, 9, 45, 0, DateTimeKind.Utc), CreatedByUserId = 7, AssignedToUserId = 5 },
                new Ticket { Id = 17, Title = "Mouse cursor freezing", Description = "Cursor freezes for a few seconds every couple of minutes.", Status = TicketStatus.Open, Priority = TicketPriority.Low, Category = TicketCategory.Hardware, CreatedAt = new DateTime(2025, 1, 22, 10, 15, 0, DateTimeKind.Utc), CreatedByUserId = 3, AssignedToUserId = null },
                new Ticket { Id = 18, Title = "Need access to marketing drive", Description = "Newly transferred employee needs access to the marketing shared drive.", Status = TicketStatus.Resolved, Priority = TicketPriority.Low, Category = TicketCategory.Account, CreatedAt = new DateTime(2025, 1, 23, 8, 0, 0, DateTimeKind.Utc), CreatedByUserId = 4, AssignedToUserId = 6 },
                new Ticket { Id = 19, Title = "Printer out of toner", Description = "Third floor printer keeps printing faded pages, likely needs a new toner cartridge.", Status = TicketStatus.Open, Priority = TicketPriority.Low, Category = TicketCategory.Hardware, CreatedAt = new DateTime(2025, 1, 23, 9, 20, 0, DateTimeKind.Utc), CreatedByUserId = 7, AssignedToUserId = null },
                new Ticket { Id = 20, Title = "Outlook not sending emails", Description = "Emails stay stuck in the outbox and never send.", Status = TicketStatus.InProgress, Priority = TicketPriority.High, Category = TicketCategory.Software, CreatedAt = new DateTime(2025, 1, 23, 11, 0, 0, DateTimeKind.Utc), CreatedByUserId = 3, AssignedToUserId = 5 },
                new Ticket { Id = 21, Title = "Two-factor authentication not working", Description = "Authenticator app codes are being rejected during login.", Status = TicketStatus.Open, Priority = TicketPriority.High, Category = TicketCategory.Account, CreatedAt = new DateTime(2025, 1, 24, 8, 40, 0, DateTimeKind.Utc), CreatedByUserId = 4, AssignedToUserId = null },
                new Ticket { Id = 22, Title = "Laptop won't turn on", Description = "Laptop shows no signs of power even when plugged in.", Status = TicketStatus.InProgress, Priority = TicketPriority.High, Category = TicketCategory.Hardware, CreatedAt = new DateTime(2025, 1, 24, 9, 10, 0, DateTimeKind.Utc), CreatedByUserId = 7, AssignedToUserId = 6 },
                new Ticket { Id = 23, Title = "Cannot open shared calendar", Description = "Shared team calendar shows an access denied error.", Status = TicketStatus.Resolved, Priority = TicketPriority.Medium, Category = TicketCategory.Account, CreatedAt = new DateTime(2025, 1, 24, 10, 30, 0, DateTimeKind.Utc), CreatedByUserId = 3, AssignedToUserId = 5 },
                new Ticket { Id = 24, Title = "Video calls freezing", Description = "Video freezes constantly during Teams calls while audio keeps working.", Status = TicketStatus.Open, Priority = TicketPriority.Medium, Category = TicketCategory.Network, CreatedAt = new DateTime(2025, 1, 25, 8, 15, 0, DateTimeKind.Utc), CreatedByUserId = 4, AssignedToUserId = null },
                new Ticket { Id = 25, Title = "Software installation blocked", Description = "Installing a required design tool is blocked by admin restrictions.", Status = TicketStatus.Closed, Priority = TicketPriority.Low, Category = TicketCategory.Software, CreatedAt = new DateTime(2025, 1, 25, 9, 50, 0, DateTimeKind.Utc), CreatedByUserId = 7, AssignedToUserId = 6 },
                new Ticket { Id = 26, Title = "Docking station not charging laptop", Description = "Laptop battery keeps draining even while connected to the docking station.", Status = TicketStatus.Open, Priority = TicketPriority.Medium, Category = TicketCategory.Hardware, CreatedAt = new DateTime(2025, 1, 25, 11, 20, 0, DateTimeKind.Utc), CreatedByUserId = 3, AssignedToUserId = null },
                new Ticket { Id = 27, Title = "Password reset email not arriving", Description = "Password reset emails are not showing up, even after checking spam.", Status = TicketStatus.InProgress, Priority = TicketPriority.Medium, Category = TicketCategory.Account, CreatedAt = new DateTime(2025, 1, 26, 8, 5, 0, DateTimeKind.Utc), CreatedByUserId = 4, AssignedToUserId = 5 },
                new Ticket { Id = 28, Title = "Slow file uploads to shared drive", Description = "Uploading files to the shared network drive takes much longer than usual.", Status = TicketStatus.Open, Priority = TicketPriority.Low, Category = TicketCategory.Network, CreatedAt = new DateTime(2025, 1, 26, 9, 30, 0, DateTimeKind.Utc), CreatedByUserId = 7, AssignedToUserId = null },
                new Ticket { Id = 29, Title = "PowerPoint crashes on export", Description = "PowerPoint crashes every time a presentation is exported to PDF.", Status = TicketStatus.Resolved, Priority = TicketPriority.Medium, Category = TicketCategory.Software, CreatedAt = new DateTime(2025, 1, 26, 10, 45, 0, DateTimeKind.Utc), CreatedByUserId = 3, AssignedToUserId = 6 },
                new Ticket { Id = 30, Title = "Need admin rights for install", Description = "Local admin rights needed to install an approved development tool.", Status = TicketStatus.Open, Priority = TicketPriority.Low, Category = TicketCategory.Account, CreatedAt = new DateTime(2025, 1, 27, 8, 25, 0, DateTimeKind.Utc), CreatedByUserId = 4, AssignedToUserId = null },
                new Ticket { Id = 31, Title = "Headset microphone not detected", Description = "New headset connects fine but the microphone is not picked up by any app.", Status = TicketStatus.InProgress, Priority = TicketPriority.Low, Category = TicketCategory.Hardware, CreatedAt = new DateTime(2025, 1, 27, 9, 40, 0, DateTimeKind.Utc), CreatedByUserId = 7, AssignedToUserId = 5 },
                new Ticket { Id = 32, Title = "VPN client won't install", Description = "VPN client installer closes immediately after launch on a new laptop.", Status = TicketStatus.Open, Priority = TicketPriority.High, Category = TicketCategory.Network, CreatedAt = new DateTime(2025, 1, 27, 11, 10, 0, DateTimeKind.Utc), CreatedByUserId = 3, AssignedToUserId = null },
                new Ticket { Id = 33, Title = "Shared mailbox missing emails", Description = "Some emails sent to the support shared mailbox never arrive.", Status = TicketStatus.Closed, Priority = TicketPriority.Medium, Category = TicketCategory.Software, CreatedAt = new DateTime(2025, 1, 28, 8, 50, 0, DateTimeKind.Utc), CreatedByUserId = 4, AssignedToUserId = 6 },
                new Ticket { Id = 34, Title = "Screen resolution resets after restart", Description = "Display resolution reverts to a lower setting every time the PC restarts.", Status = TicketStatus.Open, Priority = TicketPriority.Low, Category = TicketCategory.Hardware, CreatedAt = new DateTime(2025, 1, 28, 10, 5, 0, DateTimeKind.Utc), CreatedByUserId = 7, AssignedToUserId = null }
            );

            modelBuilder.Entity<Comment>().HasData(
                new Comment { Id = 11, TicketId = 1, UserId = 2, Text = "Checked the printer, seems to be a driver issue. Reinstalling now.", CreatedAt = new DateTime(2025, 1, 10, 10, 0, 0, DateTimeKind.Utc) },
                new Comment { Id = 12, TicketId = 2, UserId = 2, Text = "Reset the password, please try logging in again.", CreatedAt = new DateTime(2025, 1, 11, 11, 45, 0, DateTimeKind.Utc) },
                new Comment { Id = 13, TicketId = 2, UserId = 1, Text = "Still getting the same error after the reset.", CreatedAt = new DateTime(2025, 1, 11, 12, 30, 0, DateTimeKind.Utc) },
                new Comment { Id = 14, TicketId = 3, UserId = 2, Text = "ISP confirmed an outage in the area, resolved now.", CreatedAt = new DateTime(2025, 1, 8, 9, 15, 0, DateTimeKind.Utc) },
                new Comment { Id = 15, TicketId = 4, UserId = 2, Text = "License purchased and installed.", CreatedAt = new DateTime(2025, 1, 5, 16, 0, 0, DateTimeKind.Utc) },
                new Comment { Id = 16, TicketId = 5, UserId = 3, Text = "Tried a different cable, issue persists.", CreatedAt = new DateTime(2025, 1, 12, 9, 30, 0, DateTimeKind.Utc) },
                new Comment { Id = 17, TicketId = 6, UserId = 5, Text = "Investigating VPN server logs for drop causes.", CreatedAt = new DateTime(2025, 1, 13, 11, 0, 0, DateTimeKind.Utc) },
                new Comment { Id = 18, TicketId = 6, UserId = 4, Text = "Happens more often in the afternoon, for what it's worth.", CreatedAt = new DateTime(2025, 1, 13, 14, 10, 0, DateTimeKind.Utc) },
                new Comment { Id = 19, TicketId = 7, UserId = 6, Text = "Account unlocked, please try again.", CreatedAt = new DateTime(2025, 1, 14, 13, 20, 0, DateTimeKind.Utc) },
                new Comment { Id = 20, TicketId = 8, UserId = 6, Text = "Approval pending from IT budget owner.", CreatedAt = new DateTime(2025, 1, 15, 9, 0, 0, DateTimeKind.Utc) },
                new Comment { Id = 21, TicketId = 9, UserId = 5, Text = "Reconfigured mobile mail settings, sync restored.", CreatedAt = new DateTime(2025, 1, 16, 16, 0, 0, DateTimeKind.Utc) },
                new Comment { Id = 22, TicketId = 9, UserId = 4, Text = "Confirmed working on my phone now, thanks!", CreatedAt = new DateTime(2025, 1, 16, 16, 45, 0, DateTimeKind.Utc) },
                new Comment { Id = 23, TicketId = 10, UserId = 7, Text = "Coffee spilled on the keyboard yesterday, keys W, A and S are unresponsive.", CreatedAt = new DateTime(2025, 1, 17, 9, 20, 0, DateTimeKind.Utc) },
                new Comment { Id = 24, TicketId = 10, UserId = 6, Text = "Replacement keyboard ordered, should arrive tomorrow.", CreatedAt = new DateTime(2025, 1, 17, 10, 0, 0, DateTimeKind.Utc) },
                new Comment { Id = 25, TicketId = 11, UserId = 6, Text = "Checking the network share permissions after the update.", CreatedAt = new DateTime(2025, 1, 18, 11, 0, 0, DateTimeKind.Utc) },
                new Comment { Id = 26, TicketId = 11, UserId = 3, Text = "Still can't see the drive even after a restart.", CreatedAt = new DateTime(2025, 1, 18, 13, 30, 0, DateTimeKind.Utc) },
                new Comment { Id = 27, TicketId = 11, UserId = 6, Text = "Found the issue, remapping the drive letter now.", CreatedAt = new DateTime(2025, 1, 18, 15, 45, 0, DateTimeKind.Utc) },
                new Comment { Id = 28, TicketId = 12, UserId = 4, Text = "Happens on both the desktop and laptop versions of Excel.", CreatedAt = new DateTime(2025, 1, 19, 11, 40, 0, DateTimeKind.Utc) },
                new Comment { Id = 29, TicketId = 12, UserId = 5, Text = "Can you send one of the crashing files so we can reproduce it?", CreatedAt = new DateTime(2025, 1, 19, 13, 0, 0, DateTimeKind.Utc) },
                new Comment { Id = 30, TicketId = 13, UserId = 5, Text = "Access granted to the finance shared folder.", CreatedAt = new DateTime(2025, 1, 20, 8, 40, 0, DateTimeKind.Utc) },
                new Comment { Id = 31, TicketId = 13, UserId = 7, Text = "Confirmed, can see the folder now, thanks.", CreatedAt = new DateTime(2025, 1, 20, 9, 0, 0, DateTimeKind.Utc) },
                new Comment { Id = 32, TicketId = 14, UserId = 3, Text = "Docking station firmware was updated last week, monitor worked fine before that.", CreatedAt = new DateTime(2025, 1, 21, 14, 20, 0, DateTimeKind.Utc) },
                new Comment { Id = 33, TicketId = 14, UserId = 6, Text = "Rolled back the firmware, second monitor is detected again.", CreatedAt = new DateTime(2025, 1, 21, 15, 30, 0, DateTimeKind.Utc) },
                new Comment { Id = 34, TicketId = 1, UserId = 1, Text = "Printer is working again after the driver reinstall, thanks!", CreatedAt = new DateTime(2025, 1, 10, 12, 0, 0, DateTimeKind.Utc) },
                new Comment { Id = 35, TicketId = 5, UserId = 6, Text = "Swapping the monitor with a spare unit to test.", CreatedAt = new DateTime(2025, 1, 12, 11, 0, 0, DateTimeKind.Utc) },
                new Comment { Id = 36, TicketId = 8, UserId = 3, Text = "Following up, any update on the laptop approval?", CreatedAt = new DateTime(2025, 1, 17, 8, 0, 0, DateTimeKind.Utc) },
                new Comment { Id = 37, TicketId = 3, UserId = 1, Text = "Confirming the internet speed is back to normal on our end too.", CreatedAt = new DateTime(2025, 1, 8, 10, 0, 0, DateTimeKind.Utc) }
            );
        }
    }
}
