using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace asser_etude_cas.Migrations
{
    public partial class ADD_INITIAL_MIGRATION : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "aser_t_region",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aser_t_region", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "aser_t_departement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RegionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aser_t_departement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_aser_t_departement_aser_t_region_RegionId",
                        column: x => x.RegionId,
                        principalTable: "aser_t_region",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommuneEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DepartementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommuneEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommuneEntity_aser_t_departement_DepartementId",
                        column: x => x.DepartementId,
                        principalTable: "aser_t_departement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "aser_t_village",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomVillage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NbreDeMenage = table.Column<int>(type: "int", nullable: false),
                    Statut = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Longitude = table.Column<decimal>(type: "decimal(15,2)", nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(15,2)", nullable: true),
                    RegionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommuneId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aser_t_village", x => x.Id);
                    table.ForeignKey(
                        name: "FK_aser_t_village_CommuneEntity_CommuneId",
                        column: x => x.CommuneId,
                        principalTable: "CommuneEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_aser_t_departement_RegionId",
                table: "aser_t_departement",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_aser_t_village_CommuneId",
                table: "aser_t_village",
                column: "CommuneId");

            migrationBuilder.CreateIndex(
                name: "IX_CommuneEntity_DepartementId",
                table: "CommuneEntity",
                column: "DepartementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "aser_t_village");

            migrationBuilder.DropTable(
                name: "CommuneEntity");

            migrationBuilder.DropTable(
                name: "aser_t_departement");

            migrationBuilder.DropTable(
                name: "aser_t_region");
        }
    }
}
