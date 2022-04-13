using Battleship.Core.Entities;
using Battleship.Core.Enums;
using Battleship.Core.Exceptions;
using Battleship.Core.Interfaces.Repositories;
using Battleship.Core.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace Battleship.Core.Services;

public class BoardShipService : IBoardShipService
{
    private readonly IBattleshipDbContext _context;

    public BoardShipService(IBattleshipDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BoardShip>> DeploysShipsAsync(Board board)
    {
        var ships = await _context.Ships.ToListAsync();

        var takenFields = new List<int>();
        var boardShips = ships.Select(ship => PlaceOnRandomPosition(ship, takenFields)).ToList();
        board.BoardShips = boardShips;

        return boardShips;
    }

    public bool AreShipsDestroyed(IEnumerable<Move> moves, IEnumerable<BoardShip> boardShips, int boardId)
    {
        var takenFields = new List<int>();

        foreach (var ship in boardShips)
        {
            takenFields.AddRange(GetTakenFieldsByShip(ship));
        }

        return takenFields.All(field =>
            moves.Any(move => move.AttackedBoardId == boardId && move.AttackedField == field));
    }

    public IEnumerable<int> GetTakenFieldsByShip(BoardShip boardShip)
    {
        IsOutOfBound(boardShip.StartPoint, boardShip.Endpoint, boardShip.Direction);

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

    private static BoardShip PlaceOnRandomPosition(Ship ship, List<int> takenFields)
    {
        bool canPlace;
        ShipDirection direction;
        int startingField;
        int endingField;

        do
        {
            direction = GetRandomDirection();
            startingField = GetRandomFreeField(takenFields);
            endingField = direction == ShipDirection.Horizontal
                ? startingField + ship.Length - 1
                : startingField + (ship.Length - 1) * 10;

            canPlace = CanPlaceShip(startingField, endingField, direction, takenFields);
        } while (!canPlace);

        return new BoardShip
            {ShipId = ship.Id, Direction = direction, StartPoint = startingField, Endpoint = endingField};
    }

    private static bool CanPlaceShip(int startingField, int endingField, ShipDirection direction, List<int> takenFields)
    {
        try
        {
            IsOutOfBound(startingField, endingField, direction);
        }
        catch (OutOfBoundException)
        {
            return false;
        }

        var fieldsToTake = new List<int>();

        while (startingField <= endingField)
        {
            fieldsToTake.Add(startingField);

            startingField = direction == ShipDirection.Horizontal ? startingField + 1 : startingField + 10;
        }

        var canPlace = !fieldsToTake.Any(fieldToTake => takenFields.Any(takenField => takenField == fieldToTake));

        if (canPlace)
            takenFields.AddRange(fieldsToTake);

        return canPlace;
    }

    private static void IsOutOfBound(int startingField, int endingField, ShipDirection direction)
    {
        if (startingField < 0 || endingField < 0) throw OutOfBoundException.New();

        if (startingField > endingField) throw OutOfBoundException.New();

        if (endingField > 89) throw OutOfBoundException.New();

        if (direction == ShipDirection.Vertical)
        {
            var startingFieldOne = startingField % 10;
            var endingFieldOne = endingField % 10;
            
            if (startingFieldOne != endingFieldOne)
                throw OutOfBoundException.New();
        }
        else
        {
            var startingFieldTen = (startingField / 10) % 10;
            var endingFieldTen = (endingField / 10) % 10;

            if (startingFieldTen != endingFieldTen)
                throw OutOfBoundException.New();
        }
    }

    private static int GetRandomFreeField(IReadOnlyCollection<int> takenFields)
    {
        var random = new Random();
        int freeField;

        do
        {
            freeField = random.Next(0, 89);
        } while (takenFields.Any(field => field == freeField));

        return freeField;
    }

    private static ShipDirection GetRandomDirection()
    {
        var random = new Random();
        return random.Next(0, 2) == 0 ? ShipDirection.Horizontal : ShipDirection.Vertical;
    }
}