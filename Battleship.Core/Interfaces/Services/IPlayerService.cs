using Battleship.Core.Entities;

namespace Battleship.Core.Interfaces.Services;

public interface IPlayerService
{
    Task<Player> SetPlayerAsync(string name);
}