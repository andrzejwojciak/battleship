using Battleship.Core.Entities;

namespace Battleship.Core.Interfaces.ProxyRepositories;

public interface IGameProxyRepository
{
    Task<Game?> GetGameByIdAsync(string gameId);
}