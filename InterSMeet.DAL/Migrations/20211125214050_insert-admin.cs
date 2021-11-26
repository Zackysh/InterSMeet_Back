using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterSMeet.DAL.Migrations
{
    public partial class insertadmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var cusotm = @"../InterSMeet.DAL/Dumps/custom.sql";
            migrationBuilder.Sql(File.ReadAllText(cusotm));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
