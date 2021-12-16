using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterSMeet.DAL.Migrations
{
    public partial class fix_user_foreign_keys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_users_language_id",
                table: "users",
                column: "language_id");

            migrationBuilder.AddForeignKey(
                name: "FK_users_Languages_language_id",
                table: "users",
                column: "language_id",
                principalTable: "Languages",
                principalColumn: "language_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_Languages_language_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_language_id",
                table: "users");
        }
    }
}
