using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BookStore.Web.Data.Migrations
{
    public partial class PictureSizeChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "ThirdPicture",
                table: "Books",
                maxLength: 3072,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "SecondPicture",
                table: "Books",
                maxLength: 3072,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "FirstPicture",
                table: "Books",
                maxLength: 3072,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "CoverPicture",
                table: "Books",
                maxLength: 3072,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldMaxLength: 50,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "ThirdPicture",
                table: "Books",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldMaxLength: 3072,
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "SecondPicture",
                table: "Books",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldMaxLength: 3072,
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "FirstPicture",
                table: "Books",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldMaxLength: 3072,
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "CoverPicture",
                table: "Books",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldMaxLength: 3072,
                oldNullable: true);
        }
    }
}
