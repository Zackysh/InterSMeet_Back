using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterSMeet.DAL.Migrations
{
    public partial class added_business_models_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_Province_province_id",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Province",
                table: "Province");

            migrationBuilder.RenameTable(
                name: "Province",
                newName: "Provinces");

            migrationBuilder.RenameIndex(
                name: "IX_Province_name",
                table: "Provinces",
                newName: "IX_Provinces_name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Provinces",
                table: "Provinces",
                column: "province_id");

            migrationBuilder.CreateTable(
                name: "Families",
                columns: table => new
                {
                    family_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Families", x => x.family_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "Levels",
                columns: table => new
                {
                    level_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Levels", x => x.level_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "Degrees",
                columns: table => new
                {
                    degree_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    level_id = table.Column<int>(type: "int", nullable: false),
                    family_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Degrees", x => x.degree_id);
                    table.ForeignKey(
                        name: "FK_Degrees_Families_family_id",
                        column: x => x.family_id,
                        principalTable: "Families",
                        principalColumn: "family_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Degrees_Levels_level_id",
                        column: x => x.level_id,
                        principalTable: "Levels",
                        principalColumn: "level_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    student_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    birthdate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    degree_id = table.Column<int>(type: "int", nullable: false),
                    average_grades = table.Column<double>(type: "double", nullable: false),
                    image_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.student_id);
                    table.ForeignKey(
                        name: "FK_Students_Degrees_degree_id",
                        column: x => x.degree_id,
                        principalTable: "Degrees",
                        principalColumn: "degree_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_Images_image_id",
                        column: x => x.image_id,
                        principalTable: "Images",
                        principalColumn: "image_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Degrees_family_id",
                table: "Degrees",
                column: "family_id");

            migrationBuilder.CreateIndex(
                name: "IX_Degrees_level_id",
                table: "Degrees",
                column: "level_id");

            migrationBuilder.CreateIndex(
                name: "IX_Degrees_Name",
                table: "Degrees",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Families_Name",
                table: "Families",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Levels_Name",
                table: "Levels",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_degree_id",
                table: "Students",
                column: "degree_id");

            migrationBuilder.CreateIndex(
                name: "IX_Students_image_id",
                table: "Students",
                column: "image_id");

            migrationBuilder.AddForeignKey(
                name: "FK_users_Provinces_province_id",
                table: "users",
                column: "province_id",
                principalTable: "Provinces",
                principalColumn: "province_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_Provinces_province_id",
                table: "users");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Degrees");

            migrationBuilder.DropTable(
                name: "Families");

            migrationBuilder.DropTable(
                name: "Levels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Provinces",
                table: "Provinces");

            migrationBuilder.RenameTable(
                name: "Provinces",
                newName: "Province");

            migrationBuilder.RenameIndex(
                name: "IX_Provinces_name",
                table: "Province",
                newName: "IX_Province_name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Province",
                table: "Province",
                column: "province_id");

            migrationBuilder.AddForeignKey(
                name: "FK_users_Province_province_id",
                table: "users",
                column: "province_id",
                principalTable: "Province",
                principalColumn: "province_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
