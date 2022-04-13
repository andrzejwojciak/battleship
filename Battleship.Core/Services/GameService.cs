using Battleship.Core.ApiContract.RequestsModels;
using Battleship.Core.Dtos;
using Battleship.Core.Entities;
using Battleship.Core.Interfaces.ProxyRepositories;
using Battleship.Core.Interfaces.Repositories;
using Battleship.Core.Interfaces.Services;
using Battleship.Core.Results;
using Microsoft.EntityFrameworkCore;

namespace Battleship.Core.Services;

public class GameService : IGameService
{
    private readonly IPlayerService _playerService;
    private readonly IBoardService _boardService;
    private readonly IBoardShipService _boardShipService;
    private readonly IRandomMoveService _randomMoveService;
    private readonly IBattleshipDbContext _context;
    private readonly IGameProxyRepository _gameProxyRepository;

    public GameService(IPlayerService playerService, IBoardService boardService, IBattleshipDbContext context,
        IBoardShipService boardShipService, IGameProxyRepository gameProxyRepository, IRandomMoveService randomMoveService)
    {
        _playerService = playerService;
        _boardService = boardService;
        _context = context;
        _boardShipService = boardShipService;
        _gameProxyRepository = gameProxyRepository;
        _randomMoveService = randomMoveService;
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

        return GameToGameStateDto(game);
    }

    public async Task<GameStateDto?> GetGameStateDtoByIdAsync(string gameId)
    {
        var game = await _gameProxyRepository.GetGameByIdAsync(gameId);

        return game != null ? GameToGameStateDto(game) : null;
    }

    public async Task<ServiceResult<GameStateDto>> TakeRandomTurnAsync(string gameId)
    {
        var game = await _gameProxyRepository.GetGameByIdAsync(gameId);

        if (game is null)
            return ServiceResult<GameStateDto>.Failure("game doesn't exist");

        if (game.EndDateUtc is not null)
            return ServiceResult<GameStateDto>.Failure("game is finished");

        var move = _randomMoveService.GetRandomMove(game);

        await _context.Moves.AddAsync(move);
        await _context.SaveChangesAsync();

        return ServiceResult<GameStateDto>.Success(GameToGameStateDto(game));
    }

    private static GameStateDto GameToGameStateDto(Game game)
    {
        return new GameStateDto
        {
            GameId = game.Id,
            StartDateUtc = game.StartDateUtc,
            EndDateUtc = game.EndDateUtc,
            Moves = game.Moves?.Select(move => new MoveDto
                {OffensivePlayerName = move.OffensivePlayer.Name, Action = move.Action}),
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