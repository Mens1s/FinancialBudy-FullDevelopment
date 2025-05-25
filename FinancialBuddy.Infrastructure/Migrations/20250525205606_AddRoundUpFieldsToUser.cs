using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialBuddy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRoundUpFieldsToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRoundUpEnabled",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "SavingBalance",
                table: "Users",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRoundUpEnabled",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SavingBalance",
                table: "Users");
        }
    }
}
