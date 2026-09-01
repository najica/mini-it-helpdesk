using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniItHelpdesk.Migrations
{
    /// <inheritdoc />
    public partial class SeedTwentyMoreTickets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "AssignedToUserId", "Category", "CreatedAt", "CreatedByUserId", "Description", "Priority", "Status", "Title" },
                values: new object[,]
                {
                    { 15, null, "Network", new DateTime(2025, 1, 22, 8, 30, 0, 0, DateTimeKind.Utc), 4, "Laptop drops the office Wi-Fi connection every 10-15 minutes.", "Medium", "Open", "Wi-Fi keeps disconnecting" },
                    { 16, 5, "Software", new DateTime(2025, 1, 22, 9, 45, 0, 0, DateTimeKind.Utc), 7, "Slack update fails with a permissions error every time.", "Low", "InProgress", "Cannot install Slack update" },
                    { 17, null, "Hardware", new DateTime(2025, 1, 22, 10, 15, 0, 0, DateTimeKind.Utc), 3, "Cursor freezes for a few seconds every couple of minutes.", "Low", "Open", "Mouse cursor freezing" },
                    { 18, 6, "Account", new DateTime(2025, 1, 23, 8, 0, 0, 0, DateTimeKind.Utc), 4, "Newly transferred employee needs access to the marketing shared drive.", "Low", "Resolved", "Need access to marketing drive" },
                    { 19, null, "Hardware", new DateTime(2025, 1, 23, 9, 20, 0, 0, DateTimeKind.Utc), 7, "Third floor printer keeps printing faded pages, likely needs a new toner cartridge.", "Low", "Open", "Printer out of toner" },
                    { 20, 5, "Software", new DateTime(2025, 1, 23, 11, 0, 0, 0, DateTimeKind.Utc), 3, "Emails stay stuck in the outbox and never send.", "High", "InProgress", "Outlook not sending emails" },
                    { 21, null, "Account", new DateTime(2025, 1, 24, 8, 40, 0, 0, DateTimeKind.Utc), 4, "Authenticator app codes are being rejected during login.", "High", "Open", "Two-factor authentication not working" },
                    { 22, 6, "Hardware", new DateTime(2025, 1, 24, 9, 10, 0, 0, DateTimeKind.Utc), 7, "Laptop shows no signs of power even when plugged in.", "High", "InProgress", "Laptop won't turn on" },
                    { 23, 5, "Account", new DateTime(2025, 1, 24, 10, 30, 0, 0, DateTimeKind.Utc), 3, "Shared team calendar shows an access denied error.", "Medium", "Resolved", "Cannot open shared calendar" },
                    { 24, null, "Network", new DateTime(2025, 1, 25, 8, 15, 0, 0, DateTimeKind.Utc), 4, "Video freezes constantly during Teams calls while audio keeps working.", "Medium", "Open", "Video calls freezing" },
                    { 25, 6, "Software", new DateTime(2025, 1, 25, 9, 50, 0, 0, DateTimeKind.Utc), 7, "Installing a required design tool is blocked by admin restrictions.", "Low", "Closed", "Software installation blocked" },
                    { 26, null, "Hardware", new DateTime(2025, 1, 25, 11, 20, 0, 0, DateTimeKind.Utc), 3, "Laptop battery keeps draining even while connected to the docking station.", "Medium", "Open", "Docking station not charging laptop" },
                    { 27, 5, "Account", new DateTime(2025, 1, 26, 8, 5, 0, 0, DateTimeKind.Utc), 4, "Password reset emails are not showing up, even after checking spam.", "Medium", "InProgress", "Password reset email not arriving" },
                    { 28, null, "Network", new DateTime(2025, 1, 26, 9, 30, 0, 0, DateTimeKind.Utc), 7, "Uploading files to the shared network drive takes much longer than usual.", "Low", "Open", "Slow file uploads to shared drive" },
                    { 29, 6, "Software", new DateTime(2025, 1, 26, 10, 45, 0, 0, DateTimeKind.Utc), 3, "PowerPoint crashes every time a presentation is exported to PDF.", "Medium", "Resolved", "PowerPoint crashes on export" },
                    { 30, null, "Account", new DateTime(2025, 1, 27, 8, 25, 0, 0, DateTimeKind.Utc), 4, "Local admin rights needed to install an approved development tool.", "Low", "Open", "Need admin rights for install" },
                    { 31, 5, "Hardware", new DateTime(2025, 1, 27, 9, 40, 0, 0, DateTimeKind.Utc), 7, "New headset connects fine but the microphone is not picked up by any app.", "Low", "InProgress", "Headset microphone not detected" },
                    { 32, null, "Network", new DateTime(2025, 1, 27, 11, 10, 0, 0, DateTimeKind.Utc), 3, "VPN client installer closes immediately after launch on a new laptop.", "High", "Open", "VPN client won't install" },
                    { 33, 6, "Software", new DateTime(2025, 1, 28, 8, 50, 0, 0, DateTimeKind.Utc), 4, "Some emails sent to the support shared mailbox never arrive.", "Medium", "Closed", "Shared mailbox missing emails" },
                    { 34, null, "Hardware", new DateTime(2025, 1, 28, 10, 5, 0, 0, DateTimeKind.Utc), 7, "Display resolution reverts to a lower setting every time the PC restarts.", "Low", "Open", "Screen resolution resets after restart" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 34);
        }
    }
}
