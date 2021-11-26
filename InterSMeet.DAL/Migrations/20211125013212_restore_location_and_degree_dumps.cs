using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterSMeet.DAL.Migrations
{
    public partial class restore_location_and_degree_dumps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlProvinces = @"../InterSMeet.DAL/Dumps/provinces-dump.sql";
            migrationBuilder.Sql(File.ReadAllText(sqlProvinces));
            var sqlDegreesPart1 = @"../InterSMeet.DAL/Dumps/degrees-dump-1.sql";
            migrationBuilder.Sql(File.ReadAllText(sqlDegreesPart1));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
