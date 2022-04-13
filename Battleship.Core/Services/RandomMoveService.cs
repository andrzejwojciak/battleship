using Battleship.Core.Entities;
using Battleship.Core.Enums;
using Battleship.Core.Interfaces.Services;

namespace Battleship.Core.Services;

public class RandomMoveService : IRandomMoveService
{
    public Move GetRandomMove(Game game)
    {
        var lastMove = game.Moves.LastOrDefault();

        var offensivePlayer = lastMove?.AttackedBoard.Player ?? game.Boards.First().Player;
        var boardUnderAttack = game.Boards.First(board => board.PlayerId != offensivePlayer.Id);

        var attackingField = GetRandomField();

        while (!CanMakeMove(game.Moves, boardUnderAttack.Id, attackingField))
        {
            attackingField = GetRandomField();
        }

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

    private static MoveAction PerformAttack(IEnumerable<BoardShip> boardShips, int attackingField)
    {
        var takenFields = new List<int>();
        
        foreach (var boardShip in boardShips.Where(ship => !ship.Destroyed))
        {
            takenFields.AddRange(GetTakenFieldByShip(boardShip));
        }
        
        return takenFields.Any(field => field == attackingField) ? MoveAction.Boomed : MoveAction.Missed;
    }

    private static IEnumerable<int> GetTakenFieldByShip(BoardShip boardShip)
    {
        var takenFields = new List<int>();

        var startingField = boardShip.StartPoint;
        var endingField = boardShip.Endpoint;
        var direction = boardShip.Direction;

        while (startingField <= endingField)
        {
            takenFields.Add(startingField);

            startingField = direction == ShipDirection.Horizontal ? startingField + 1 : startingField + 10;
        }
        
        return takenFields;
    }

    private static bool CanMakeMove(IEnumerable<Move> moves, int boardUnderAttackId, int attackingField) =>
        !moves.Any(move => move.AttackedBoardId == boardUnderAttackId && move.AttackedField == attackingField);

    private static int GetRandomField()
    {
        var random = new Random();
        return random.Next(0, 89);
    }
}