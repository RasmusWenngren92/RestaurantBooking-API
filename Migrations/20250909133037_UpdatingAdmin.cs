using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestauantBookingAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Admin_Username",
                table: "Admins");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Admins",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Admins",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Admins");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Admins",
                newName: "Username");

            migrationBuilder.CreateIndex(
                name: "IX_Admin_Username",
                table: "Admins",
                column: "Username",
                unique: true);
        }
    }
}
