using Battleship.Core.ApiContract.RequestsModels;
using Battleship.Core.Dtos;
using Battleship.Core.Entities;
using Battleship.Core.Enums;
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
        IBoardShipService boardShipService, IGameProxyRepository gameProxyRepository,
        IRandomMoveService randomMoveService)
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
        var game = new Game {Id = Guid.NewGuid().ToString().Replace("-", ""), StartDateUtc = DateTime.UtcNow};
        var player1 = await _playerService.SetPlayerAsync(createGameModel.Player1Name);
        var player2 = await _playerService.SetPlayerAsync(createGameModel.Player2Name);

        var boards = await _boardService.CreateBoardsAsync(game.Id, new[] {player1, player2});

        foreach (var board in boards)
        {
            await _boardShipService.DeploysShipsAsync(board);
        }

        await _gameProxyRepository.SaveGameStateAsync(game);
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
            return ServiceResult<GameStateDto>.Failure("game is over");

        var move = _randomMoveService.GetRandomMove(game);
        game.Moves.Add(move);

        if (move.Action == MoveAction.Boomed)
        {
            var attackedBoard = game.Boards.First(board => board.Id == move.AttackedBoardId);
            var areBoardDestroyed =
                _boardShipService.AreShipsDestroyed(game.Moves, attackedBoard.BoardShips, move.AttackedBoardId);

            if (areBoardDestroyed)
            {
                var lastMove = new Move
                {
                    GameId = game.Id,
                    Action = MoveAction.DestroyedAllEnemyShips,
                    AttackedBoardId = move.AttackedBoardId,
                    AttackedField = null,
                    OffensivePlayerId = move.OffensivePlayerId
                };

                game.EndDateUtc = DateTime.UtcNow;
                game.Moves.Add(lastMove);
            }
        }

        await _gameProxyRepository.UpdateGameStateAsync(game);

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
            {
                OffensivePlayerName = move.OffensivePlayer.Name,
                AttackedField = move.AttackedField,
                Action = move.Action
            }),
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
}