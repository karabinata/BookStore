using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Web.Data.Migrations
{
    public partial class OrdersTableChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Shipping",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Subtotal",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "Orders",
                newName: "Price");

            migrationBuilder.AddColumn<string>(
                name: "TraderId",
                table: "Orders",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TraderId",
                table: "Orders",
                column: "TraderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_TraderId",
                table: "Orders",
                column: "TraderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_TraderId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_TraderId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TraderId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Orders",
                newName: "Total");

            migrationBuilder.AddColumn<decimal>(
                name: "Shipping",
                table: "Orders",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Subtotal",
                table: "Orders",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
