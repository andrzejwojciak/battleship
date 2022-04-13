using Battleship.Core.ApiContract.RequestsModels;
using Battleship.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Battleship.UI.Controllers;

[ApiController]
[Route("games")]
public class GamesController : ControllerBase
{
    private readonly IGameService _gameService;

    public GamesController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateGame([FromBody] CreateGameModel request)
    {
        var gameStateDto = await _gameService.CreateGameAsync(request);
        return Ok(gameStateDto);
    }

    [HttpGet("{gameId}")]
    public async Task<IActionResult> GetGameById([FromRoute] string gameId)
    {
        var gameStateDto = await _gameService.GetGameStateDtoByIdAsync(gameId);

        return gameStateDto is not null
            ? Ok(gameStateDto)
            : NotFound();
    }

    [HttpPost("{gameId}/random-move")]
    public async Task<IActionResult> MakeMove([FromRoute] string gameId)
    {
        var serviceResult = await _gameService.TakeRandomTurnAsync(gameId);

        return serviceResult.Succeeded
            ? Ok(serviceResult.Data)
            : BadRequest(new {serviceResult.Errors});
    }
}