using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterSMeet.DAL.Migrations
{
    public partial class update_user_role_fk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_user_roles_role_id",
                table: "users");

            migrationBuilder.AlterColumn<int>(
                name: "role_id",
                table: "users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_users_user_roles_role_id",
                table: "users",
                column: "role_id",
                principalTable: "user_roles",
                principalColumn: "role_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_user_roles_role_id",
                table: "users");

            migrationBuilder.AlterColumn<int>(
                name: "role_id",
                table: "users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_users_user_roles_role_id",
                table: "users",
                column: "role_id",
                principalTable: "user_roles",
                principalColumn: "role_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
