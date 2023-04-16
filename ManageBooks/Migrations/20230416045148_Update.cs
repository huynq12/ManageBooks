using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManageBooks.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ordering",
                table: "Customer");

            migrationBuilder.AddColumn<int>(
                name: "OrderingStatus",
                table: "Customer",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderingStatus",
                table: "Customer");

            migrationBuilder.AddColumn<bool>(
                name: "Ordering",
                table: "Customer",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
