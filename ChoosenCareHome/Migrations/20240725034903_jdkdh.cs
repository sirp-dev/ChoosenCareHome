using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChoosenCareHome.Migrations
{
    /// <inheritdoc />
    public partial class jdkdh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Accepted",
                table: "UserTimeSheets");

            migrationBuilder.AddColumn<string>(
                name: "AcceptedReason",
                table: "UserTimeSheets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TimesheetAcceptance",
                table: "UserTimeSheets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptedReason",
                table: "UserTimeSheets");

            migrationBuilder.DropColumn(
                name: "TimesheetAcceptance",
                table: "UserTimeSheets");

            migrationBuilder.AddColumn<bool>(
                name: "Accepted",
                table: "UserTimeSheets",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
