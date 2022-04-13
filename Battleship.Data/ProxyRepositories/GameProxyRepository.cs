using Battleship.Core.Entities;
using Battleship.Core.Interfaces.ProxyRepositories;
using Battleship.Core.Interfaces.Repositories;
using Battleship.Data.Cache.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Battleship.Data.ProxyRepositories;

public class GameProxyRepository : IGameProxyRepository
{
    private readonly IGameCache _gameCache;
    private readonly IBattleshipDbContext _context;

    public GameProxyRepository(IGameCache gameCache, IBattleshipDbContext context)
    {
        _gameCache = gameCache;
        _context = context;
    }

    public async Task<Game?> GetGameByIdAsync(string gameId)
    {
        var game = await _context.Games
            .Include(game => game.Boards)
            .ThenInclude(board => board.Player)
            .Include(game => game.Boards)
            .ThenInclude(board => board.BoardShips)
            .ThenInclude(boardShip => boardShip.Ship)
            .Include(game => game.Moves)
            .FirstOrDefaultAsync(game => game.Id == gameId);

        return game;
    }
}