using Battleship.Core.Enums;

namespace Battleship.Core.Entities;

public class BoardShip
{
    public int Id { get; set; }
    public int BoardId { get; set; }
    public int ShipId { get; set; }
    public ShipDirection Direction { get; set; }
    public bool Destroyed { get; set; }
    public int StartPoint { get; set; }
    public int Endpoint { get; set; }
}