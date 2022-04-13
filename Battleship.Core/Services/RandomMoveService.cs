using Battleship.Core.Entities;
using Battleship.Core.Enums;
using Battleship.Core.Interfaces.Services;

namespace Battleship.Core.Services;

public class RandomMoveService : IRandomMoveService
{
    private readonly IBoardShipService _boardShipService;

    public RandomMoveService(IBoardShipService boardShipService)
    {
        _boardShipService = boardShipService;
    }

    public Move GetRandomMove(Game game)
    {
        var lastMove = game.Moves.LastOrDefault();

        var offensivePlayer = lastMove?.AttackedBoard.Player ?? game.Boards.First().Player;
        var boardUnderAttack = game.Boards.First(board => board.PlayerId != offensivePlayer.Id);

        var attackingField = RandomFieldToAttack(game.Moves, boardUnderAttack.Id);

        var attackResult = PerformAttack(boardUnderAttack.BoardShips, attackingField);

        var newMove = new Move
        {
            OffensivePlayerId = offensivePlayer.Id,
            GameId = game.Id,
            AttackedBoardId = boardUnderAttack.Id,
            AttackedField = attackingField,
            Action = attackResult
        };

        return newMove;
    }

    private MoveAction PerformAttack(IEnumerable<BoardShip> boardShips, int attackingField)
    {
        var takenFields = new List<int>();
        
        foreach (var boardShip in boardShips.Where(ship => !ship.Destroyed))
        {
            takenFields.AddRange(_boardShipService.GetTakenFieldsByShip(boardShip));
        }
        
        return takenFields.Any(field => field == attackingField) ? MoveAction.Boomed : MoveAction.Missed;
    }

    private static int RandomFieldToAttack(IEnumerable<Move> moves, int boardUnderAttackId)
    {
        var random = new Random();
        
        var attackingField = random.Next(0, 89); 

        while (!CanAttackField(moves, boardUnderAttackId, attackingField))
        {
            attackingField = random.Next(0, 89);
        }
        
        return attackingField;
    }
    
    private static bool CanAttackField(IEnumerable<Move> moves, int boardUnderAttackId, int attackingField) =>
        !moves.Any(move => move.AttackedBoardId == boardUnderAttackId && move.AttackedField == attackingField);
}