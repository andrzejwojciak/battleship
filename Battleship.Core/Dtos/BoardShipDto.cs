using Battleship.Core.Enums;

namespace Battleship.Core.Dtos;

public class BoardShipDto
{
    public string Name { get; set; }
    public string Direction { get; set; }
    public int StartPoint { get; set; }
    public int Endpoint { get; set; }
}