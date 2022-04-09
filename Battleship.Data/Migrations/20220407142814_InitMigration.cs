using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Battleship.Data.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StartDateUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDateUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Length = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ships", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GameId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Boards_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Boards_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoardShips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BoardId = table.Column<int>(type: "int", nullable: false),
                    ShipId = table.Column<int>(type: "int", nullable: false),
                    Direction = table.Column<int>(type: "int", nullable: false),
                    Destroyed = table.Column<bool>(type: "bit", nullable: false),
                    StartPoint = table.Column<int>(type: "int", nullable: false),
                    Endpoint = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardShips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoardShips_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardShips_Ships_ShipId",
                        column: x => x.ShipId,
                        principalTable: "Ships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Moves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OffensivePlayerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GameId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AttackedBoardId = table.Column<int>(type: "int", nullable: false),
                    Field = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<int>(type: "int", nullable: false),
                    DestroyedShipName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Moves_Boards_AttackedBoardId",
                        column: x => x.AttackedBoardId,
                        principalTable: "Boards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Moves_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Moves_Players_OffensivePlayerId",
                        column: x => x.OffensivePlayerId,
                        principalTable: "Players",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Ships",
                columns: new[] { "Id", "Length", "Name" },
                values: new object[,]
                {
                    { 1, 5, "Carrier" },
                    { 2, 4, "Battleship" },
                    { 3, 3, "Cruiser" },
                    { 4, 3, "Submarine" },
                    { 5, 2, "Destroyer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Boards_GameId",
                table: "Boards",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Boards_PlayerId",
                table: "Boards",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardShips_BoardId",
                table: "BoardShips",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardShips_ShipId",
                table: "BoardShips",
                column: "ShipId");

            migrationBuilder.CreateIndex(
                name: "IX_Moves_AttackedBoardId",
                table: "Moves",
                column: "AttackedBoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Moves_GameId",
                table: "Moves",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Moves_OffensivePlayerId",
                table: "Moves",
                column: "OffensivePlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardShips");

            migrationBuilder.DropTable(
                name: "Moves");

            migrationBuilder.DropTable(
                name: "Ships");

            migrationBuilder.DropTable(
                name: "Boards");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
