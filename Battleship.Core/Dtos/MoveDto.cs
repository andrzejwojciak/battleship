using Battleship.Core.Enums;

namespace Battleship.Core.Dtos;

public class MoveDto
{
    public Guid PlayerId { get; set; }
    public MoveAction MoveAction { get; set; }
}