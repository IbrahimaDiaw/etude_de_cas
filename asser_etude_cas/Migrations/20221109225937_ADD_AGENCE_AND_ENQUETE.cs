using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace asser_etude_cas.Migrations
{
    public partial class ADD_AGENCE_AND_ENQUETE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "aser_t_agence",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CodeAgent = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aser_t_agence", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "aser_t_enquete_periodique",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NbreMenagesRecenses = table.Column<int>(type: "int", nullable: false),
                    TauxAccesParMenage = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    TauxCouvertureParVillage = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    VillageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AgentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aser_t_enquete_periodique", x => x.Id);
                    table.ForeignKey(
                        name: "FK_aser_t_enquete_periodique_aser_t_village_VillageId",
                        column: x => x.VillageId,
                        principalTable: "aser_t_village",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_aser_t_enquete_periodique_VillageId",
                table: "aser_t_enquete_periodique",
                column: "VillageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "aser_t_agence");

            migrationBuilder.DropTable(
                name: "aser_t_enquete_periodique");
        }
    }
}
