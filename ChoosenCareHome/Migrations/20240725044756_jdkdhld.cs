using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChoosenCareHome.Migrations
{
    /// <inheritdoc />
    public partial class jdkdhld : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UserSheetEndTime",
                table: "UserTimeSheets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UserSheetStartTime",
                table: "UserTimeSheets",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserSheetEndTime",
                table: "UserTimeSheets");

            migrationBuilder.DropColumn(
                name: "UserSheetStartTime",
                table: "UserTimeSheets");
        }
    }
}
