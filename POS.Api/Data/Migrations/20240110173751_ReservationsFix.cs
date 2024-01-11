using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReservationsFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Order_OrderId",
                table: "Reservation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_OrderId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_TableId",
                table: "Reservation");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Reservation",
                newName: "Id");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "Table",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ReservationId",
                table: "Order",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Table_OrderId",
                table: "Table",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_TableId",
                table: "Reservation",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ReservationId",
                table: "Order",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Reservation_ReservationId",
                table: "Order",
                column: "ReservationId",
                principalTable: "Reservation",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Table_Order_OrderId",
                table: "Table",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Reservation_ReservationId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Table_Order_OrderId",
                table: "Table");

            migrationBuilder.DropIndex(
                name: "IX_Table_OrderId",
                table: "Table");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_TableId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Order_ReservationId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Table");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Reservation",
                newName: "OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation",
                columns: new[] { "TableId", "OrderId" });

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_OrderId",
                table: "Reservation",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_TableId",
                table: "Reservation",
                column: "TableId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Order_OrderId",
                table: "Reservation",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
