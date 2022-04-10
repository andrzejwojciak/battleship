using Battleship.Core.Entities;

namespace Battleship.Core.Interfaces.Services;

public interface IBoardService
{
    Task<IEnumerable<Board>> CreateBoardsAsync(string gameId, IEnumerable<Player> players);
}