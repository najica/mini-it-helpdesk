using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniItHelpdesk.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreTicketsAndComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "CreatedAt", "Text", "TicketId", "UserId" },
                values: new object[,]
                {
                    { 34, new DateTime(2025, 1, 10, 12, 0, 0, 0, DateTimeKind.Utc), "Printer is working again after the driver reinstall, thanks!", 1, 1 },
                    { 35, new DateTime(2025, 1, 12, 11, 0, 0, 0, DateTimeKind.Utc), "Swapping the monitor with a spare unit to test.", 5, 6 },
                    { 36, new DateTime(2025, 1, 17, 8, 0, 0, 0, DateTimeKind.Utc), "Following up, any update on the laptop approval?", 8, 3 },
                    { 37, new DateTime(2025, 1, 8, 10, 0, 0, 0, DateTimeKind.Utc), "Confirming the internet speed is back to normal on our end too.", 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "AssignedToUserId", "Category", "CreatedAt", "CreatedByUserId", "Description", "Priority", "Status", "Title" },
                values: new object[,]
                {
                    { 10, null, "Hardware", new DateTime(2025, 1, 17, 9, 10, 0, 0, DateTimeKind.Utc), 7, "Several keys on the keyboard stopped working after a spill.", "Low", "Open", "Keyboard keys not responding" },
                    { 11, 6, "Network", new DateTime(2025, 1, 18, 10, 40, 0, 0, DateTimeKind.Utc), 3, "Shared network drive is not showing up after the latest update.", "High", "InProgress", "Cannot access shared drive" },
                    { 12, null, "Software", new DateTime(2025, 1, 19, 11, 25, 0, 0, DateTimeKind.Utc), 4, "Excel crashes consistently when opening files larger than 20MB.", "Medium", "Open", "Excel crashes on large files" },
                    { 13, 5, "Account", new DateTime(2025, 1, 20, 8, 15, 0, 0, DateTimeKind.Utc), 7, "New team member needs read access to the finance shared folder.", "Low", "Resolved", "Request access to finance folder" },
                    { 14, 6, "Hardware", new DateTime(2025, 1, 21, 14, 0, 0, 0, DateTimeKind.Utc), 3, "Second monitor is not detected after docking station firmware update.", "Medium", "Closed", "Second monitor not detected" }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "CreatedAt", "Text", "TicketId", "UserId" },
                values: new object[,]
                {
                    { 23, new DateTime(2025, 1, 17, 9, 20, 0, 0, DateTimeKind.Utc), "Coffee spilled on the keyboard yesterday, keys W, A and S are unresponsive.", 10, 7 },
                    { 24, new DateTime(2025, 1, 17, 10, 0, 0, 0, DateTimeKind.Utc), "Replacement keyboard ordered, should arrive tomorrow.", 10, 6 },
                    { 25, new DateTime(2025, 1, 18, 11, 0, 0, 0, DateTimeKind.Utc), "Checking the network share permissions after the update.", 11, 6 },
                    { 26, new DateTime(2025, 1, 18, 13, 30, 0, 0, DateTimeKind.Utc), "Still can't see the drive even after a restart.", 11, 3 },
                    { 27, new DateTime(2025, 1, 18, 15, 45, 0, 0, DateTimeKind.Utc), "Found the issue, remapping the drive letter now.", 11, 6 },
                    { 28, new DateTime(2025, 1, 19, 11, 40, 0, 0, DateTimeKind.Utc), "Happens on both the desktop and laptop versions of Excel.", 12, 4 },
                    { 29, new DateTime(2025, 1, 19, 13, 0, 0, 0, DateTimeKind.Utc), "Can you send one of the crashing files so we can reproduce it?", 12, 5 },
                    { 30, new DateTime(2025, 1, 20, 8, 40, 0, 0, DateTimeKind.Utc), "Access granted to the finance shared folder.", 13, 5 },
                    { 31, new DateTime(2025, 1, 20, 9, 0, 0, 0, DateTimeKind.Utc), "Confirmed, can see the folder now, thanks.", 13, 7 },
                    { 32, new DateTime(2025, 1, 21, 14, 20, 0, 0, DateTimeKind.Utc), "Docking station firmware was updated last week, monitor worked fine before that.", 14, 3 },
                    { 33, new DateTime(2025, 1, 21, 15, 30, 0, 0, DateTimeKind.Utc), "Rolled back the firmware, second monitor is detected again.", 14, 6 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 14);
        }
    }
}
