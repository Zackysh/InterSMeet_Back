using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterSMeet.DAL.Migrations
{
    public partial class addrolesandlangs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = @"../InterSMeet.DAL/Dumps/roles-and-langs-dump.sql";
            migrationBuilder.Sql(File.ReadAllText(sql));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
