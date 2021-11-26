using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterSMeet.DAL.Migrations
{
    public partial class update_student_avatar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Images_image_id",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "image_id",
                table: "Students",
                newName: "avatar_id");

            migrationBuilder.RenameIndex(
                name: "IX_Students_image_id",
                table: "Students",
                newName: "IX_Students_avatar_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Images_avatar_id",
                table: "Students",
                column: "avatar_id",
                principalTable: "Images",
                principalColumn: "image_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Images_avatar_id",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "avatar_id",
                table: "Students",
                newName: "image_id");

            migrationBuilder.RenameIndex(
                name: "IX_Students_avatar_id",
                table: "Students",
                newName: "IX_Students_image_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Images_image_id",
                table: "Students",
                column: "image_id",
                principalTable: "Images",
                principalColumn: "image_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
