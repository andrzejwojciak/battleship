using Battleship.Core.Entities;

namespace Battleship.Core.Interfaces.ProxyRepositories;

public interface IGameProxyRepository
{
    Task<Game?> GetGameByIdAsync(string gameId);
    Task SaveGameStateAsync(Game game);
    Task UpdateGameStateAsync(Game game);
}