using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniItHelpdesk.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordHashToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "TEXT",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$LaHBE4wxxt2gjQUIVdnODu.Q6XfErT/61N/G/hW.RqV.RuoskmGS.");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$LaHBE4wxxt2gjQUIVdnODu.Q6XfErT/61N/G/hW.RqV.RuoskmGS.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Users");
        }
    }
}
