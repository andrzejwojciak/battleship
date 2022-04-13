using Battleship.Core.Enums;

namespace Battleship.Core.Dtos;

public class MoveDto
{
    public string OffensivePlayerName { get; set; }
    public MoveAction Action { get; set; }
}