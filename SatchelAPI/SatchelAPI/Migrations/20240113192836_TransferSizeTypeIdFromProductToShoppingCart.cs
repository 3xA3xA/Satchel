using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SatchelAPI.Migrations
{
    /// <inheritdoc />
    public partial class TransferSizeTypeIdFromProductToShoppingCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_SizeTypes_SizeTypeId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SizeTypeId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SizeTypeId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "SizeTypeId",
                table: "ShoppingCarts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_SizeTypeId",
                table: "ShoppingCarts",
                column: "SizeTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_SizeTypes_SizeTypeId",
                table: "ShoppingCarts",
                column: "SizeTypeId",
                principalTable: "SizeTypes",
                principalColumn: "SizeTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_SizeTypes_SizeTypeId",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCarts_SizeTypeId",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "SizeTypeId",
                table: "ShoppingCarts");

            migrationBuilder.AddColumn<int>(
                name: "SizeTypeId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_SizeTypeId",
                table: "Products",
                column: "SizeTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SizeTypes_SizeTypeId",
                table: "Products",
                column: "SizeTypeId",
                principalTable: "SizeTypes",
                principalColumn: "SizeTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
