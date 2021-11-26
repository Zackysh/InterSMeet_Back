using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterSMeet.DAL.Migrations
{
    public partial class fix_business_model_3_ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Families",
                newName: "name");

            migrationBuilder.RenameIndex(
                name: "IX_Families_Name",
                table: "Families",
                newName: "IX_Families_name");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Degrees",
                newName: "name");

            migrationBuilder.RenameIndex(
                name: "IX_Degrees_Name",
                table: "Degrees",
                newName: "IX_Degrees_name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "Families",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_Families_name",
                table: "Families",
                newName: "IX_Families_Name");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Degrees",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_Degrees_name",
                table: "Degrees",
                newName: "IX_Degrees_Name");
        }
    }
}
