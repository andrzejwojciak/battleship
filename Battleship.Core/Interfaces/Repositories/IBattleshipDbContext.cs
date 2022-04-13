using Battleship.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Battleship.Core.Interfaces.Repositories;

public interface IBattleshipDbContext
{
    DbSet<Board> Boards { get; set; }
    DbSet<BoardShip> BoardShips { get; set; }
    DbSet<Game> Games { get; set; }
    DbSet<Move> Moves { get; set; }
    DbSet<Player> Players { get; set; }
    DbSet<Ship> Ships { get; set; }

    Task SaveChangesAsync();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}