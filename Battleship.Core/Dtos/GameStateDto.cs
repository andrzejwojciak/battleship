namespace Battleship.Core.Dtos;

public class GameStateDto
{
    public string GameId { get; set; }
    public IEnumerable<BoardDto> Boards { get; set; }
    public IEnumerable<MoveDto>? Moves { get; set; }
    public DateTime StartDateUtc { get; set; }
    public DateTime? EndDateUtc { get; set; }
}