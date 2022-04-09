using Battleship.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Battleship.Data.Configurations;

public class BoardConfiguration : IEntityTypeConfiguration<Board>
{
    public void Configure(EntityTypeBuilder<Board> builder)
    {
        builder
            .HasKey(board => board.Id);

        builder
            .HasOne(board => board.Game)
            .WithMany(game => game.Boards)
            .HasForeignKey(board => board.GameId);
        
        builder
            .HasOne(board => board.Player)
            .WithMany(player => player.Boards)
            .HasForeignKey(board => board.PlayerId);
    }
}