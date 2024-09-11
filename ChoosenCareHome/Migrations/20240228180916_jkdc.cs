using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChoosenCareHome.Migrations
{
    /// <inheritdoc />
    public partial class jkdc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeSheets_AspNetUsers_UserId",
                table: "TimeSheets");

            migrationBuilder.DropIndex(
                name: "IX_TimeSheets_UserId",
                table: "TimeSheets");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "TimeSheets");

            migrationBuilder.DropColumn(
                name: "Break",
                table: "TimeSheets");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "TimeSheets");

            migrationBuilder.DropColumn(
                name: "PostCode",
                table: "TimeSheets");

            migrationBuilder.DropColumn(
                name: "RatePerHour",
                table: "TimeSheets");

            migrationBuilder.DropColumn(
                name: "Report",
                table: "TimeSheets");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "TimeSheets");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TimeSheets");

            migrationBuilder.CreateTable(
                name: "UserTimeSheets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Report = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RatePerHour = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Break = table.Column<int>(type: "int", nullable: false),
                    TimeSheetId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTimeSheets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTimeSheets_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserTimeSheets_TimeSheets_TimeSheetId",
                        column: x => x.TimeSheetId,
                        principalTable: "TimeSheets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTimeSheets_TimeSheetId",
                table: "UserTimeSheets",
                column: "TimeSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTimeSheets_UserId",
                table: "UserTimeSheets",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTimeSheets");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "TimeSheets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Break",
                table: "TimeSheets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "EndTime",
                table: "TimeSheets",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "PostCode",
                table: "TimeSheets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RatePerHour",
                table: "TimeSheets",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Report",
                table: "TimeSheets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "StartTime",
                table: "TimeSheets",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TimeSheets",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheets_UserId",
                table: "TimeSheets",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSheets_AspNetUsers_UserId",
                table: "TimeSheets",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
