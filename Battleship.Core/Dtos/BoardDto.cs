namespace Battleship.Core.Dtos;

public class BoardDto
{
    public string PlayerName { get; set; }
    public IEnumerable<BoardShipDto> Ships { get; set; }
}