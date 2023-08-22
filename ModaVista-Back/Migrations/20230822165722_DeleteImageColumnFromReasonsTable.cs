using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModaVista_Back.Migrations
{
    public partial class DeleteImageColumnFromReasonsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Reasons");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Reasons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
