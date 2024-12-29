using Microsoft.EntityFrameworkCore.Migrations;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shopping_Cart_Book.Data.Migrations
{
    /// <inheritdoc />
    public partial class changecol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CartId",
                table: "ShoppingCart");

            migrationBuilder.DropColumn(
                name: "ShoppingCart_Id",
                table: "CartDetail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "ShoppingCart",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShoppingCart_Id",
                table: "CartDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
