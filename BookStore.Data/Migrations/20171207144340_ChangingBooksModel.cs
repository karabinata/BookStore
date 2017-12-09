using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BookStore.Web.Data.Migrations
{
    public partial class ChangingBooksModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OneOrMoreBookUnits",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Authors");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Authors",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "BooksAvailable",
                table: "Books",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BooksAvailable",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Authors",
                newName: "LastName");

            migrationBuilder.AddColumn<bool>(
                name: "OneOrMoreBookUnits",
                table: "Books",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Authors",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
