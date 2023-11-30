using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThucTapLTSEDU.Migrations
{
    /// <inheritdoc />
    public partial class TT_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_item_product_productId",
                table: "Cart_item");

            migrationBuilder.DropColumn(
                name: "product_id",
                table: "Cart_item");

            migrationBuilder.AlterColumn<int>(
                name: "productId",
                table: "Cart_item",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_item_product_productId",
                table: "Cart_item",
                column: "productId",
                principalTable: "product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_item_product_productId",
                table: "Cart_item");

            migrationBuilder.AlterColumn<int>(
                name: "productId",
                table: "Cart_item",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "product_id",
                table: "Cart_item",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_item_product_productId",
                table: "Cart_item",
                column: "productId",
                principalTable: "product",
                principalColumn: "Id");
        }
    }
}
