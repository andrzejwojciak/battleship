using Battleship.Core.Entities;
using Battleship.Core.Enums;
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

        await _context.BoardShips.AddRangeAsync(boardShips);
        await _context.SaveChangesAsync();

        return boardShips;
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
        if (endingField > 89) return false;

        if (direction == ShipDirection.Horizontal)
        {
            var startingFieldTen = (startingField/10)%10;
            var endingFieldTen = (endingField/10)%10;

            if (startingFieldTen != endingFieldTen)
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