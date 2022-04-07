namespace Battleship.Core.Entities;

public class Player
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string GameId { get; set; }
    
    public Board Board { get; set; }
}