using Battleship.Core.ApiContract.RequestsModels;
using Battleship.Core.Dtos;
using Battleship.Core.Entities;

namespace Battleship.Core.Interfaces.Services;

public interface IGameService
{
    Task<GameStateDto> CreateGameAsync(CreateGameModel model);
}