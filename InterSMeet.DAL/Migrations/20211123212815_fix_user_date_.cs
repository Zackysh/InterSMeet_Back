using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterSMeet.DAL.Migrations
{
    public partial class fix_user_date_ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedAt",
                table: "users",
                type: "longtext",
                nullable: false,
                collation: "utf8mb4_0900_ai_ci")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UpdatedAt",
                table: "users",
                type: "longtext",
                nullable: false,
                collation: "utf8mb4_0900_ai_ci")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "users");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "users");
        }
    }
}
