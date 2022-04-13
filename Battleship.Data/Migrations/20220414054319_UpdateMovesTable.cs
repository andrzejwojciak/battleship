using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Battleship.Data.Migrations
{
    public partial class UpdateMovesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DestroyedShipName",
                table: "Moves");

            migrationBuilder.DropColumn(
                name: "Field",
                table: "Moves");

            migrationBuilder.AddColumn<int>(
                name: "AttackedField",
                table: "Moves",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttackedField",
                table: "Moves");

            migrationBuilder.AddColumn<string>(
                name: "DestroyedShipName",
                table: "Moves",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Field",
                table: "Moves",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
