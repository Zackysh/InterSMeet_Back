using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterSMeet.DAL.Migrations
{
    public partial class add_student_user_fk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Images_avatar_id",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "avatar_id",
                table: "Students",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Students_user_id",
                table: "Students",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Images_avatar_id",
                table: "Students",
                column: "avatar_id",
                principalTable: "Images",
                principalColumn: "image_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_users_user_id",
                table: "Students",
                column: "user_id",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Images_avatar_id",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_users_user_id",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_user_id",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "avatar_id",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Images_avatar_id",
                table: "Students",
                column: "avatar_id",
                principalTable: "Images",
                principalColumn: "image_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
