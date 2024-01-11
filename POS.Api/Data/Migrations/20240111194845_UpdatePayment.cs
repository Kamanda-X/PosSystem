using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipAmount",
                table: "Payment");

            migrationBuilder.AddColumn<float>(
                name: "TipAmount",
                table: "Order",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipAmount",
                table: "Order");

            migrationBuilder.AddColumn<float>(
                name: "TipAmount",
                table: "Payment",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
