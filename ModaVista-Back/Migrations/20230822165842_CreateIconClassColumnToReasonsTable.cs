using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModaVista_Back.Migrations
{
    public partial class CreateIconClassColumnToReasonsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IconClass",
                table: "Reasons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IconClass",
                table: "Reasons");
        }
    }
}
