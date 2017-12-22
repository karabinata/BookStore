using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Web.Data.Migrations
{
    public partial class BooksTableChangedAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsNew",
                table: "Books");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsNew",
                table: "Books",
                nullable: false,
                defaultValue: false);
        }
    }
}
