using Battleship.Core.Entities;

namespace Battleship.Core.Interfaces.Services;

public interface IBoardShipService
{
    Task<IEnumerable<BoardShip>> DeploysShipsAsync(Board board);
}