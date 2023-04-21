using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManageBooks.Migrations
{
    /// <inheritdoc />
    public partial class addOrderCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderingStatus",
                table: "Customer",
                newName: "Status");

            migrationBuilder.AddColumn<int>(
                name: "OrderCount",
                table: "Book",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderCount",
                table: "Book");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Customer",
                newName: "OrderingStatus");
        }
    }
}
