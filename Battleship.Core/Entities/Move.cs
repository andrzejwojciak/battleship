using Battleship.Core.Enums;

namespace Battleship.Core.Entities;

public class Move
{
    public int Id { get; set; }
    public string OffensivePlayerId { get; set; }
    public string GameId { get; set; }
    public int AttackedBoardId { get; set; }
    public MoveAction Action { get; set; }
    public int Field { get; set; }
}