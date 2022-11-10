using Microsoft.EntityFrameworkCore.Migrations;

namespace asser_etude_cas.Migrations
{
    public partial class ADD_INTITULE_IN_ENQUETE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Intitule",
                table: "aser_t_enquete_periodique",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Intitule",
                table: "aser_t_enquete_periodique");
        }
    }
}
