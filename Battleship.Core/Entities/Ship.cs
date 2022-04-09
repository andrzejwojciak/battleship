namespace Battleship.Core.Entities;

public class Ship
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Length { get; set; }

    public IEnumerable<BoardShip> BoardShips { get; set; }
}