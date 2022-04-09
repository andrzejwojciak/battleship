namespace Battleship.Core.Entities;

public class Board
{
    public int Id { get; set; }
    public string PlayerId { get; set; }
    public string GameId { get; set; }

    public Game Game { get; set; }
    public Player Player { get; set; }
    public IEnumerable<BoardShip> BoardShips { get; set; }
    public IEnumerable<Move> Moves { get; set; }
}