using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantBookingAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddTableBookingConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Customers_CustomerId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Tables_TableId",
                table: "Bookings");

            migrationBuilder.AlterColumn<bool>(
                name: "IsAvailable",
                table: "Tables",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Bookings",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_IsAvailable",
                table: "Tables",
                column: "IsAvailable");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_SeatingCapacity",
                table: "Tables",
                column: "SeatingCapacity");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_TableNumber",
                table: "Tables",
                column: "TableNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_AvailabilityCheck",
                table: "Bookings",
                columns: new[] { "TableId", "StartDateTime", "Status" },
                filter: "[Status] != 'Cancelled'");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CustomerBookings",
                table: "Bookings",
                columns: new[] { "CustomerId", "StartDateTime" });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_StartDateTime",
                table: "Bookings",
                column: "StartDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_Status",
                table: "Bookings",
                column: "Status");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Customers",
                table: "Bookings",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Tables",
                table: "Bookings",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Customers",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Tables",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Tables_IsAvailable",
                table: "Tables");

            migrationBuilder.DropIndex(
                name: "IX_Tables_SeatingCapacity",
                table: "Tables");

            migrationBuilder.DropIndex(
                name: "IX_Tables_TableNumber",
                table: "Tables");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_AvailabilityCheck",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_CustomerBookings",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_StartDateTime",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_Status",
                table: "Bookings");

            migrationBuilder.AlterColumn<bool>(
                name: "IsAvailable",
                table: "Tables",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Bookings",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Customers_CustomerId",
                table: "Bookings",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Tables_TableId",
                table: "Bookings",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
