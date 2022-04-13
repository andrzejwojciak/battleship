using Battleship.Core.ApiContract.RequestsModels;
using Battleship.Core.Dtos;
using Battleship.Core.Entities;
using Battleship.Core.Results;

namespace Battleship.Core.Interfaces.Services;

public interface IGameService
{
    Task<GameStateDto> CreateGameAsync(CreateGameModel model);
    Task<GameStateDto?> GetGameStateDtoByIdAsync(string gameId);
    Task<ServiceResult<GameStateDto>> TakeRandomTurnAsync(string gameId);
}