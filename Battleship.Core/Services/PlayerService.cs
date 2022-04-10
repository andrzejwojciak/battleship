using Battleship.Core.Entities;
using Battleship.Core.Interfaces.Repositories;
using Battleship.Core.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace Battleship.Core.Services;

public class PlayerService : IPlayerService
{
    private readonly IBattleshipDbContext _context;

    public PlayerService(IBattleshipDbContext context)
    {
        _context = context;
    }

    public async Task<Player> SetPlayerAsync(string name)
    {
        var player = await _context.Players.SingleOrDefaultAsync(player => player.Name.Equals(name));

        if (player != null)
            return player;

        player = new Player {Id = Guid.NewGuid().ToString(), Name = name};

        await _context.Players.AddAsync(player);
        await _context.SaveChangesAsync();

        return player;
    }
}