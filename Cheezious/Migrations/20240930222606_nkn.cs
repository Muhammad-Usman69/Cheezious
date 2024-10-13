using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cheezious.Migrations
{
    /// <inheritdoc />
    public partial class nkn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "AssignOrders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AssignProductId",
                table: "AssignOrders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssignOrders_AssignProductId",
                table: "AssignOrders",
                column: "AssignProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignOrders_Products_AssignProductId",
                table: "AssignOrders",
                column: "AssignProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignOrders_Products_AssignProductId",
                table: "AssignOrders");

            migrationBuilder.DropIndex(
                name: "IX_AssignOrders_AssignProductId",
                table: "AssignOrders");

            migrationBuilder.DropColumn(
                name: "AssignProductId",
                table: "AssignOrders");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "AssignOrders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
