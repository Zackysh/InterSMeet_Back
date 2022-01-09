using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterSMeet.DAL.Migrations
{

    public partial class fixforeignkeys : Migration

    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_offers_offers_offer_id",
                table: "offer_degree",
                column: "offer_id",
                principalTable: "offers",
                principalColumn: "offer_id");
            migrationBuilder.AddForeignKey(
                name: "FK_degrees_degrees_degree_id",
                table: "offer_degree",
                column: "degree_id",
                principalTable: "degrees",
                principalColumn: "degree_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
