namespace Battleship.Core.Entities;

public class Player
{
    public Player()
    {
        Moves = new HashSet<Move>();
    }

    public string Id { get; set; }
    public string Name { get; set; }

    public IEnumerable<Board> Boards { get; set; }
    public IEnumerable<Move> Moves { get; set; }
}