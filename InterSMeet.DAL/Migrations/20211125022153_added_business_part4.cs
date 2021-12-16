using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterSMeet.DAL.Migrations
{
    public partial class added_business_part4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlDegreesPart2 = @"../InterSMeet.DAL/Dumps/degrees-dump-2.sql";
            migrationBuilder.Sql(File.ReadAllText(sqlDegreesPart2));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
