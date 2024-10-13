using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cheezious.Migrations
{
    /// <inheritdoc />
    public partial class order : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignOrders_Orders_AssingOrderId",
                table: "AssignOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignOrders_Products_ProductId",
                table: "AssignOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignOrders_Users_BuyerId",
                table: "AssignOrders");

            migrationBuilder.DropIndex(
                name: "IX_AssignOrders_AssingOrderId",
                table: "AssignOrders");

            migrationBuilder.DropIndex(
                name: "IX_AssignOrders_BuyerId",
                table: "AssignOrders");

            migrationBuilder.DropColumn(
                name: "AssingOrderId",
                table: "AssignOrders");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                table: "AssignOrders");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "AssignOrders",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_AssignOrders_ProductId",
                table: "AssignOrders",
                newName: "IX_AssignOrders_OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignOrders_Orders_OrderId",
                table: "AssignOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignOrders_Orders_OrderId",
                table: "AssignOrders");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "AssignOrders",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_AssignOrders_OrderId",
                table: "AssignOrders",
                newName: "IX_AssignOrders_ProductId");

            migrationBuilder.AddColumn<int>(
                name: "AssingOrderId",
                table: "AssignOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BuyerId",
                table: "AssignOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AssignOrders_AssingOrderId",
                table: "AssignOrders",
                column: "AssingOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignOrders_BuyerId",
                table: "AssignOrders",
                column: "BuyerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignOrders_Orders_AssingOrderId",
                table: "AssignOrders",
                column: "AssingOrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignOrders_Products_ProductId",
                table: "AssignOrders",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignOrders_Users_BuyerId",
                table: "AssignOrders",
                column: "BuyerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
