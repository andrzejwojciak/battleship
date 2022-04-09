using Battleship.Core.Enums;
using Battleship.Core.Services;

namespace Battleship.Core.Entities;

public class Move
{
    public int Id { get; set; }
    public string OffensivePlayerId { get; set; }
    public string GameId { get; set; }
    public int AttackedBoardId { get; set; }
    public int Field { get; set; }
    public MoveAction Action { get; set; }
    public string? DestroyedShipName { get; set; }

    public Game Game { get; set; }
    public Player OffensivePlayer { get; set; }
    public Board AttackedBoard { get; set; }
}