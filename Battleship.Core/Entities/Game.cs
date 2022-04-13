namespace Battleship.Core.Entities;

public class Game
{
    public string Id { get; set; }
    public DateTime StartDateUtc { get; set; }
    public DateTime? EndDateUtc { get; set; }

    public ICollection<Board> Boards { get; set; }
    public ICollection<Move> Moves { get; set; }
}