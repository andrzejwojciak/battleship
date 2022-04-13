namespace Battleship.Core.Entities;

public class Board
{
    public int Id { get; set; }
    public string PlayerId { get; set; }
    public string GameId { get; set; }

    public Game Game { get; set; }
    public Player Player { get; set; }
    public ICollection<BoardShip> BoardShips { get; set; }
    public ICollection<Move> Moves { get; set; }
}