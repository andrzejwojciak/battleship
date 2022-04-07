namespace Battleship.Core.Entities;

public class Board
{
    public int Id { get; set; }
    public Guid PlayerId { get; set; }
    public Guid GameId { get; set; }

    public Game Game { get; set; }
    public Player Player { get; set; }
}