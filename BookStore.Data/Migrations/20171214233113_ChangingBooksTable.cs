using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Web.Data.Migrations
{
    public partial class ChangingBooksTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstPicture",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Format",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Heigth",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ISBN",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Information",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "KeyWords",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "NotesForTraider",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "PaintorName",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "SecondPicture",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "SeriesAndLibraries",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Subtitle",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ThirdPicture",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "TranslatorName",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Тhickness",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "Weigth",
                table: "Books",
                newName: "NumberOfPages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumberOfPages",
                table: "Books",
                newName: "Weigth");

            migrationBuilder.AddColumn<byte[]>(
                name: "FirstPicture",
                table: "Books",
                maxLength: 3145728,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Format",
                table: "Books",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Heigth",
                table: "Books",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ISBN",
                table: "Books",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Information",
                table: "Books",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KeyWords",
                table: "Books",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NotesForTraider",
                table: "Books",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaintorName",
                table: "Books",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "SecondPicture",
                table: "Books",
                maxLength: 3145728,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeriesAndLibraries",
                table: "Books",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subtitle",
                table: "Books",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ThirdPicture",
                table: "Books",
                maxLength: 3145728,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TranslatorName",
                table: "Books",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Width",
                table: "Books",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Тhickness",
                table: "Books",
                nullable: true);
        }
    }
}
