using Microsoft.AspNetCore.Mvc;

namespace Battleship.UI.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController : ControllerBase
{
    [HttpGet]
    public IActionResult StartGame()
    {
        return Ok();
    }
}