using System;
using System.Collections.Generic;
using Battleship.Core.Entities;
using Battleship.Core.Enums;
using Battleship.Core.Exceptions;
using Battleship.Core.Interfaces.Services;
using Battleship.Core.Services;
using Battleship.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Battleship.UnitTests.Services;

public class BoardShipServiceTests
{
    private readonly IBoardShipService _boardShipService;
 
    public BoardShipServiceTests()
    {
        var options = new DbContextOptionsBuilder<BattleshipDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
 
        var context = new BattleshipDbContext(options);
        _boardShipService = new BoardShipService(context);
    }
    
    [Fact]
    public void GetTakenFieldsByShip_ShouldReturnTakenFields()
    {
        // Arrange
        var boardShips = new List<BoardShip>()
        {
            new()
            {
                StartPoint = 1,
                Endpoint = 4,
                Direction = ShipDirection.Horizontal
            },
            new()
            {
                StartPoint = 87,
                Endpoint = 89,
                Direction = ShipDirection.Horizontal
            },
            new()
            {
                StartPoint = 25,
                Endpoint = 45,
                Direction = ShipDirection.Vertical
            },
            new()
            {
                StartPoint = 79,
                Endpoint = 89,
                Direction = ShipDirection.Vertical
            }
        };
        
        // Act
        var boardShip1TakenField = _boardShipService.GetTakenFieldsByShip(boardShips[0]);
        var boardShip2TakenField = _boardShipService.GetTakenFieldsByShip(boardShips[1]);
        var boardShip3TakenField = _boardShipService.GetTakenFieldsByShip(boardShips[2]);
        var boardShip4TakenField = _boardShipService.GetTakenFieldsByShip(boardShips[3]);
        
        // Assert
        Assert.Equal(new List<int>{1, 2, 3, 4}, boardShip1TakenField);
        Assert.Equal(new List<int>{87, 88, 89}, boardShip2TakenField);
        Assert.Equal(new List<int>{25, 35, 45}, boardShip3TakenField);
        Assert.Equal(new List<int>{79, 89}, boardShip4TakenField);
    }
    
    [Fact]
    public void GetTakenFieldsByShip_ShouldThrowOutOfBoundException()
    {
        // Arrange
        var boardShips = new List<BoardShip>()
        {
            new()
            {
                StartPoint = -4,
                Endpoint = 1001,
                Direction = ShipDirection.Horizontal
            },
            new()
            {
                StartPoint = 87,
                Endpoint = 90,
                Direction = ShipDirection.Horizontal
            },
            new()
            {
                StartPoint = 78,
                Endpoint = 81,
                Direction = ShipDirection.Horizontal
            },
            new()
            {
                StartPoint = 4,
                Endpoint = 14,
                Direction = ShipDirection.Horizontal
            },
            new()
            {
                StartPoint = 79,
                Endpoint = 90,
                Direction = ShipDirection.Vertical
            },
            new()
            {
                StartPoint = 4,
                Endpoint = 15,
                Direction = ShipDirection.Vertical
            },
            new()
            {
                StartPoint = 21,
                Endpoint = 11,
                Direction = ShipDirection.Vertical
            },
        };

        foreach (var boardShip in boardShips)
        {
            // Act
            var action = () =>
            {
                _boardShipService.GetTakenFieldsByShip(boardShip);
            };

            // Assert
            Assert.Throws<OutOfBoundException>(action);
        }
    }
}