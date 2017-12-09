using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Web.Data.Migrations
{
    public partial class AddingTraderIdInBooksTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TraderId",
                table: "Books",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_TraderId",
                table: "Books",
                column: "TraderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_AspNetUsers_TraderId",
                table: "Books",
                column: "TraderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_AspNetUsers_TraderId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_TraderId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "TraderId",
                table: "Books");
        }
    }
}
