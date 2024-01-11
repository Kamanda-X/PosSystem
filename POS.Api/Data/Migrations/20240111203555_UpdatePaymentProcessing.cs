using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePaymentProcessing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Discount");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Payment",
                newName: "Total");

            migrationBuilder.AddColumn<float>(
                name: "AmountPaid",
                table: "Payment",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Change",
                table: "Payment",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Discount",
                table: "Payment",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Sum",
                table: "Payment",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountPaid",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "Change",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "Sum",
                table: "Payment");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "Payment",
                newName: "Amount");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Discount",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
