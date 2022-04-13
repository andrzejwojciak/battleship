using Battleship.Core.Entities;

namespace Battleship.Data.Cache.Interfaces;

public interface IGameCache
{
    IEnumerable<Game> Games { get; set; }
}