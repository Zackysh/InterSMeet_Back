using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterSMeet.DAL.Migrations
{
    public partial class fix_image_data_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Images_image_data",
                table: "Images");

            migrationBuilder.AlterColumn<byte[]>(
                name: "image_data",
                table: "Images",
                type: "longblob",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(3072)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "image_data",
                table: "Images",
                type: "varbinary(3072)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "longblob");

            migrationBuilder.CreateIndex(
                name: "IX_Images_image_data",
                table: "Images",
                column: "image_data",
                unique: true);
        }
    }
}
