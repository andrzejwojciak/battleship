using System.ComponentModel.DataAnnotations;
using Battleship.Core.Validations;

namespace Battleship.Core.ApiContract.RequestsModels;

public class CreateGameModel
{
    [Required] 
    public string Player1Name { get; set; }

    [Required]
    [Unlike(nameof(Player1Name), ErrorMessage = "Player2Name must be other than Player1Name.")]
    public string Player2Name { get; set; }
}