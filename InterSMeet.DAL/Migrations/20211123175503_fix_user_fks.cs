using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterSMeet.DAL.Migrations
{
    public partial class fix_user_fks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Languages",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "language",
                table: "users");

            migrationBuilder.AddColumn<int>(
                name: "language_id",
                table: "users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "language_id",
                table: "Languages",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Languages",
                table: "Languages",
                column: "language_id");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Languages",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "language_id",
                table: "users");

            migrationBuilder.DropColumn(
                name: "language_id",
                table: "Languages");

            migrationBuilder.AddColumn<string>(
                name: "language",
                table: "users",
                type: "varchar(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "",
                collation: "utf8mb4_0900_ai_ci")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Languages",
                table: "Languages",
                column: "name");
        }
    }
}
