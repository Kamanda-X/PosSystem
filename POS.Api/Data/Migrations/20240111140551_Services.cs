using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class Services : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Table_TableId",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "ReservedDate",
                table: "Reservation");

            migrationBuilder.AlterColumn<Guid>(
                name: "TableId",
                table: "Reservation",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<DateTime>(
                name: "End",
                table: "Reservation",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Start",
                table: "Reservation",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "TimeSlotId",
                table: "Reservation",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TimeSlot",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Start = table.Column<TimeOnly>(type: "time", nullable: false),
                    End = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSlot", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_TimeSlotId",
                table: "Reservation",
                column: "TimeSlotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Table_TableId",
                table: "Reservation",
                column: "TableId",
                principalTable: "Table",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_TimeSlot_TimeSlotId",
                table: "Reservation",
                column: "TimeSlotId",
                principalTable: "TimeSlot",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Table_TableId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_TimeSlot_TimeSlotId",
                table: "Reservation");

            migrationBuilder.DropTable(
                name: "TimeSlot");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_TimeSlotId",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "End",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "Start",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "TimeSlotId",
                table: "Reservation");

            migrationBuilder.AlterColumn<Guid>(
                name: "TableId",
                table: "Reservation",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ReservedDate",
                table: "Reservation",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Table_TableId",
                table: "Reservation",
                column: "TableId",
                principalTable: "Table",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
