using Battleship.Core.Entities;

namespace Battleship.Core.Interfaces.Services;

public interface IRandomMoveService
{
    Move GetRandomMove(Game game);
}