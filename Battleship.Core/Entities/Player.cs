namespace Battleship.Core.Entities;

public class Player
{
    public Player()
    {
        Moves = new HashSet<Move>();
    }

    public string Id { get; set; }
    public string Name { get; set; }

    public ICollection<Board> Boards { get; set; }
    public ICollection<Move> Moves { get; set; }
}