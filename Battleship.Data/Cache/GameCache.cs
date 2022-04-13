using Battleship.Core.Entities;
using Battleship.Data.Cache.Interfaces;

namespace Battleship.Data.Cache;

public class GameCache : IGameCache
{
    public IEnumerable<Game> Games { get; set; }
}