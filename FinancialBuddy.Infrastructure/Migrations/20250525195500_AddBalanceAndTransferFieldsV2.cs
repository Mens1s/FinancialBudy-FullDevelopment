using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialBuddy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBalanceAndTransferFieldsV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromAccount",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "ToAccount",
                table: "Transfers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FromAccount",
                table: "Transfers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ToAccount",
                table: "Transfers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
