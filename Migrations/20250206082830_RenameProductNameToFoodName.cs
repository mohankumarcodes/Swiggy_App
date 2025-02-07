using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Swiggy_App.Migrations
{
    /// <inheritdoc />
    public partial class RenameProductNameToFoodName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "Orders",
                newName: "FoodName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FoodName",
                table: "Orders",
                newName: "ProductName");
        }
    }
}
