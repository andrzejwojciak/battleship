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

            migrationBuilder.RenameColumn(
                name: "Field",
                table: "Moves",
                newName: "AttackedField");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AttackedField",
                table: "Moves",
                newName: "Field");

            migrationBuilder.AddColumn<string>(
                name: "DestroyedShipName",
                table: "Moves",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
