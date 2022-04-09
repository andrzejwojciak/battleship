using System.Reflection;
using Battleship.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Battleship.Data;

public class BattleshipDbContext : DbContext
{
    public BattleshipDbContext(DbContextOptions<BattleshipDbContext> options)
        : base(options)
    {
    }

    public DbSet<Board> Boards { get; set; }
    public DbSet<BoardShip> BoardShips { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Move> Moves { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Ship> Ships { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Ship>().HasData(
            new Ship {Id = 1, Name = "Carrier", Length = 5},
            new Ship {Id = 2, Name = "Battleship", Length = 4},
            new Ship {Id = 3, Name = "Cruiser", Length = 3},
            new Ship {Id = 4, Name = "Submarine", Length = 3},
            new Ship {Id = 5, Name = "Destroyer", Length = 2}
        );
    }
}