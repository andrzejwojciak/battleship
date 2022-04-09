using Battleship.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Battleship.Data.Configurations;

public class BoardShipConfiguration : IEntityTypeConfiguration<BoardShip>
{
    public void Configure(EntityTypeBuilder<BoardShip> builder)
    {
        builder
            .HasKey(boardShip => boardShip.Id);

        builder
            .HasOne(boardShip => boardShip.Board)
            .WithMany(board => board.BoardShips)
            .HasForeignKey(boardShip => boardShip.BoardId);
        
        builder
            .HasOne(boardShip => boardShip.Ship)
            .WithMany(ship => ship.BoardShips)
            .HasForeignKey(boardShip => boardShip.ShipId);
    }
}