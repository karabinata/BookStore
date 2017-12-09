using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Web.Data.Migrations
{
    public partial class PublisherUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Publishers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Publishers",
                nullable: true);
        }
    }
}
