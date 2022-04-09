using Battleship.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Battleship.Data.Configurations;

public class MoveConfiguration : IEntityTypeConfiguration<Move>
{
    public void Configure(EntityTypeBuilder<Move> builder)
    {
        builder
            .HasKey(move => move.Id);

        builder
            .HasOne(move => move.AttackedBoard)
            .WithMany(board => board.Moves)
            .HasForeignKey(move => move.AttackedBoardId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(move => move.OffensivePlayer)
            .WithMany(player => player.Moves)
            .HasForeignKey(move => move.OffensivePlayerId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder
            .HasOne(move => move.Game)
            .WithMany(player => player.Moves)
            .HasForeignKey(move => move.GameId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}