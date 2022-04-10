using Battleship.Core.ApiContract.RequestsModels;
using Battleship.Core.Dtos;
using Battleship.Core.Entities;
using Battleship.Core.Interfaces.ProxyRepositories;
using Battleship.Core.Interfaces.Repositories;
using Battleship.Core.Interfaces.Services;

namespace Battleship.Core.Services;

public class GameService : IGameService
{
    private readonly IPlayerService _playerService;
    private readonly IBoardService _boardService;
    private readonly IBoardShipService _boardShipService;
    private readonly IBattleshipDbContext _context;
    private readonly IGameProxyRepository _gameProxyRepository;

    public GameService(IPlayerService playerService, IBoardService boardService, IBattleshipDbContext context,
        IBoardShipService boardShipService, IGameProxyRepository gameProxyRepository)
    {
        _playerService = playerService;
        _boardService = boardService;
        _context = context;
        _boardShipService = boardShipService;
        _gameProxyRepository = gameProxyRepository;
    }

    public async Task<GameStateDto> CreateGameAsync(CreateGameModel createGameModel)
    {
        var game = await CreateGameAsync();
        var player1 = await _playerService.SetPlayerAsync(createGameModel.Player1Name);
        var player2 = await _playerService.SetPlayerAsync(createGameModel.Player2Name);

        var boards = await _boardService.CreateBoardsAsync(game.Id, new[] {player1, player2});

        foreach (var board in boards)
        {
            await _boardShipService.DeploysShipsAsync(board);
        }

        return new GameStateDto
        {
            GameId = game.Id,
            StartDateUtc = game.StartDateUtc,
            Boards = game.Boards.Select(board => new BoardDto
            {
                PlayerName = board.Player.Name,
                Ships = board.BoardShips.Select(ship => new BoardShipDto
                {
                    StartPoint = ship.StartPoint, 
                    Endpoint = ship.Endpoint, 
                    Direction = ship.Direction.ToString(), 
                    Name = ship.Ship.Name
                })
            })
        };
    }

    private async Task<Game> CreateGameAsync()
    {
        var game = new Game {Id = Guid.NewGuid().ToString().Replace("-", ""), StartDateUtc = DateTime.UtcNow};
        await _context.Games.AddAsync(game);
        await _context.SaveChangesAsync();
        return game;
    }
}