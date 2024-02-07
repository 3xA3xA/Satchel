using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SatchelAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddSizeTypeToProductAndIncreaseImagePathLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                table: "ProductImages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.CreateTable(
                name: "SizeTypeToProducts",
                columns: table => new
                {
                    SizeTypeToProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SizeTypeId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SizeTypeToProducts", x => x.SizeTypeToProductId);
                    table.ForeignKey(
                        name: "FK_SizeTypeToProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SizeTypeToProducts_SizeTypes_SizeTypeId",
                        column: x => x.SizeTypeId,
                        principalTable: "SizeTypes",
                        principalColumn: "SizeTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SizeTypeToProducts_ProductId",
                table: "SizeTypeToProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SizeTypeToProducts_SizeTypeId",
                table: "SizeTypeToProducts",
                column: "SizeTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SizeTypeToProducts");

            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                table: "ProductImages",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
