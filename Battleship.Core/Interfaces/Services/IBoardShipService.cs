using Battleship.Core.Entities;

namespace Battleship.Core.Interfaces.Services;

public interface IBoardShipService
{
    Task<IEnumerable<BoardShip>> DeploysShipsAsync(Board board);
    bool AreShipsDestroyed(IEnumerable<Move> moves, IEnumerable<BoardShip> boardShips, int boardId);
    IEnumerable<int> GetTakenFieldsByShip(BoardShip boardShip);
}