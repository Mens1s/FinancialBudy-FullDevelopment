using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialBuddy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIsAutoPaymentToSubscription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAutoPayment",
                table: "Subscriptions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAutoPayment",
                table: "Subscriptions");
        }
    }
}
