using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterSMeet.DAL.Migrations
{
    public partial class fix_user_date : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_user_roles_role_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_role_id",
                table: "users");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "users");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "users",
                type: "timestamp(3)",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP(3)");

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "users",
                type: "timestamp(3)",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP(3)");

            migrationBuilder.CreateIndex(
                name: "IX_users_role_id",
                table: "users",
                column: "role_id");

            migrationBuilder.AddForeignKey(
                name: "FK_users_user_roles_role_id",
                table: "users",
                column: "role_id",
                principalTable: "user_roles",
                principalColumn: "role_id");
        }
    }
}
