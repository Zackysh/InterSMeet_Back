using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterSMeet.DAL.Migrations
{
    public partial class added_business_models_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "users",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "users",
                newName: "created_at");

            migrationBuilder.AlterColumn<byte[]>(
                name: "image_data",
                table: "Images",
                type: "varbinary(3072)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "longblob");

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_username",
                table: "users",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_roles_name",
                table: "user_roles",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Languages_name",
                table: "Languages",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_image_data",
                table: "Images",
                column: "image_data",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_users_email",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_username",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_user_roles_name",
                table: "user_roles");

            migrationBuilder.DropIndex(
                name: "IX_Languages_name",
                table: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_Images_image_data",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "users",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "users",
                newName: "CreatedAt");

            migrationBuilder.AlterColumn<byte[]>(
                name: "image_data",
                table: "Images",
                type: "longblob",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(3072)");
        }
    }
}
