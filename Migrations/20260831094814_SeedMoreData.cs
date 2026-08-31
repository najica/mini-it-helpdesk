using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniItHelpdesk.Migrations
{
    /// <inheritdoc />
    public partial class SeedMoreData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "CreatedAt", "Text", "TicketId", "UserId" },
                values: new object[,]
                {
                    { 11, new DateTime(2025, 1, 10, 10, 0, 0, 0, DateTimeKind.Utc), "Checked the printer, seems to be a driver issue. Reinstalling now.", 1, 2 },
                    { 12, new DateTime(2025, 1, 11, 11, 45, 0, 0, DateTimeKind.Utc), "Reset the password, please try logging in again.", 2, 2 },
                    { 13, new DateTime(2025, 1, 11, 12, 30, 0, 0, DateTimeKind.Utc), "Still getting the same error after the reset.", 2, 1 },
                    { 14, new DateTime(2025, 1, 8, 9, 15, 0, 0, DateTimeKind.Utc), "ISP confirmed an outage in the area, resolved now.", 3, 2 },
                    { 15, new DateTime(2025, 1, 5, 16, 0, 0, 0, DateTimeKind.Utc), "License purchased and installed.", 4, 2 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "PasswordHash", "Role" },
                values: new object[,]
                {
                    { 3, "ana.petrovic@test.com", "Ana Petrović", "$2a$11$LaHBE4wxxt2gjQUIVdnODu.Q6XfErT/61N/G/hW.RqV.RuoskmGS.", "Employee" },
                    { 4, "marko.jovanovic@test.com", "Marko Jovanović", "$2a$11$LaHBE4wxxt2gjQUIVdnODu.Q6XfErT/61N/G/hW.RqV.RuoskmGS.", "Employee" },
                    { 5, "jelena.nikolic@test.com", "Jelena Nikolić", "$2a$11$LaHBE4wxxt2gjQUIVdnODu.Q6XfErT/61N/G/hW.RqV.RuoskmGS.", "ITAgent" },
                    { 6, "stefan.ilic@test.com", "Stefan Ilić", "$2a$11$LaHBE4wxxt2gjQUIVdnODu.Q6XfErT/61N/G/hW.RqV.RuoskmGS.", "ITAgent" },
                    { 7, "milica.stankovic@test.com", "Milica Stanković", "$2a$11$LaHBE4wxxt2gjQUIVdnODu.Q6XfErT/61N/G/hW.RqV.RuoskmGS.", "Employee" },
                    { 8, "admin@test.com", "Admin User", "$2a$11$LaHBE4wxxt2gjQUIVdnODu.Q6XfErT/61N/G/hW.RqV.RuoskmGS.", "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "AssignedToUserId", "Category", "CreatedAt", "CreatedByUserId", "Description", "Priority", "Status", "Title" },
                values: new object[,]
                {
                    { 5, null, "Hardware", new DateTime(2025, 1, 12, 9, 0, 0, 0, DateTimeKind.Utc), 3, "External monitor flickers randomly, possibly a cable or driver issue.", "Low", "Open", "Monitor flickering" },
                    { 6, 5, "Network", new DateTime(2025, 1, 13, 10, 20, 0, 0, DateTimeKind.Utc), 4, "VPN disconnects every few minutes when working from home.", "High", "InProgress", "VPN connection drops" },
                    { 7, 6, "Account", new DateTime(2025, 1, 14, 13, 5, 0, 0, DateTimeKind.Utc), 7, "Account got locked after several failed login attempts, needs unlocking.", "Medium", "Resolved", "Account locked out" },
                    { 8, null, "Hardware", new DateTime(2025, 1, 15, 8, 45, 0, 0, DateTimeKind.Utc), 3, "Current laptop is too slow for daily tasks, requesting a replacement.", "Medium", "Open", "New laptop request" },
                    { 9, 5, "Software", new DateTime(2025, 1, 16, 15, 30, 0, 0, DateTimeKind.Utc), 4, "Emails are not syncing on mobile device, only on desktop.", "Low", "Closed", "Email sync issues" }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "CreatedAt", "Text", "TicketId", "UserId" },
                values: new object[,]
                {
                    { 16, new DateTime(2025, 1, 12, 9, 30, 0, 0, DateTimeKind.Utc), "Tried a different cable, issue persists.", 5, 3 },
                    { 17, new DateTime(2025, 1, 13, 11, 0, 0, 0, DateTimeKind.Utc), "Investigating VPN server logs for drop causes.", 6, 5 },
                    { 18, new DateTime(2025, 1, 13, 14, 10, 0, 0, DateTimeKind.Utc), "Happens more often in the afternoon, for what it's worth.", 6, 4 },
                    { 19, new DateTime(2025, 1, 14, 13, 20, 0, 0, DateTimeKind.Utc), "Account unlocked, please try again.", 7, 6 },
                    { 20, new DateTime(2025, 1, 15, 9, 0, 0, 0, DateTimeKind.Utc), "Approval pending from IT budget owner.", 8, 6 },
                    { 21, new DateTime(2025, 1, 16, 16, 0, 0, 0, DateTimeKind.Utc), "Reconfigured mobile mail settings, sync restored.", 9, 5 },
                    { 22, new DateTime(2025, 1, 16, 16, 45, 0, 0, DateTimeKind.Utc), "Confirmed working on my phone now, thanks!", 9, 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
