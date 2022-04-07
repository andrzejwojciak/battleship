namespace Battleship.Core.Entities;

public class Game
{
    public string Id { get; set; }
    public DateTime StartDateUtc { get; set; }
    public DateTime? EndDateUtc { get; set; }

    public IEnumerable<Board> Boards { get; set; }
    public IEnumerable<Move> Moves { get; set; }
}