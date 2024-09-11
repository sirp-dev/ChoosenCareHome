using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChoosenCareHome.Migrations
{
    /// <inheritdoc />
    public partial class addo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "GeneratedInvoice",
                table: "UserTimeSheets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "InvoiceNumber",
                table: "UserTimeSheets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvoiceNumber",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GeneratedInvoice",
                table: "UserTimeSheets");

            migrationBuilder.DropColumn(
                name: "InvoiceNumber",
                table: "UserTimeSheets");

            migrationBuilder.DropColumn(
                name: "InvoiceNumber",
                table: "Invoices");
        }
    }
}
