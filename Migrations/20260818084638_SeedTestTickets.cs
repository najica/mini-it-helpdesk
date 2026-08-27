using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable


namespace MiniItHelpdesk.Migrations
{
    public partial class SeedTestTickets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "AssignedToUserId", "Category", "CreatedAt", "CreatedByUserId", "Description", "Priority", "Status", "Title" },
                values: new object[,]
                {
                    { 1, null, "Hardware", new DateTime(2025, 1, 10, 9, 30, 0, 0, DateTimeKind.Utc), 1, "The printer on the second floor is unresponsive, likely a driver issue.", "Medium", "Open", "Printer not working" },
                    { 2, 2, "Software", new DateTime(2025, 1, 11, 11, 15, 0, 0, DateTimeKind.Utc), 1, "Login returns 'Invalid credentials' error even though the password is correct.", "High", "InProgress", "Cannot log into the system" },
                    { 3, 2, "Network", new DateTime(2025, 1, 8, 8, 0, 0, 0, DateTimeKind.Utc), 2, "Internet has been extremely slow since this morning, affecting the whole team.", "Low", "Resolved", "Slow internet connection" },
                    { 4, 2, "Software", new DateTime(2025, 1, 5, 14, 45, 0, 0, DateTimeKind.Utc), 1, "Adobe Photoshop license needed for a new employee.", null, "Closed", "New software license needed" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "AssignedToUserId", "Category", "CreatedAt", "CreatedByUserId", "Description", "Priority", "Status", "Title" },
                values: new object[,]
                {
                    { 11, null, "Hardware", new DateTime(2025, 1, 10, 9, 30, 0, 0, DateTimeKind.Utc), 1, "The printer on the second floor is unresponsive, likely a driver issue.", "Medium", "Open", "Printer not working" },
                    { 12, 2, "Software", new DateTime(2025, 1, 11, 11, 15, 0, 0, DateTimeKind.Utc), 1, "Login returns 'Invalid credentials' error even though the password is correct.", "High", "InProgress", "Cannot log into the system" },
                    { 13, 2, "Network", new DateTime(2025, 1, 8, 8, 0, 0, 0, DateTimeKind.Utc), 2, "Internet has been extremely slow since this morning, affecting the whole team.", "Low", "Resolved", "Slow internet connection" },
                    { 14, 2, "Software", new DateTime(2025, 1, 5, 14, 45, 0, 0, DateTimeKind.Utc), 1, "Adobe Photoshop license needed for a new employee.", null, "Closed", "New software license needed" }
                });
        }
    }
}
