using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterSMeet.DAL.Migrations
{
    public partial class fix_business_model_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Levels",
                newName: "name");

            migrationBuilder.RenameIndex(
                name: "IX_Levels_Name",
                table: "Levels",
                newName: "IX_Levels_name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "Levels",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_Levels_name",
                table: "Levels",
                newName: "IX_Levels_Name");
        }
    }
}
