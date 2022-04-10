using Battleship.Core.Entities;
using Battleship.Core.Interfaces.Repositories;
using Battleship.Core.Interfaces.Services;

namespace Battleship.Core.Services;

public class BoardService : IBoardService
{
    private readonly IBattleshipDbContext _context;

    public BoardService(IBattleshipDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Board>> CreateBoardsAsync(string gameId, IEnumerable<Player> players)
    {
        var boards = players.Select(player => new Board {PlayerId = player.Id, GameId = gameId}).ToList();

        await _context.Boards.AddRangeAsync(boards);
        await _context.SaveChangesAsync();

        return boards;
    }
}