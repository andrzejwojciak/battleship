using Battleship.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Battleship.Data.Configurations;

public class ShipConfiguration : IEntityTypeConfiguration<Ship>
{
    public void Configure(EntityTypeBuilder<Ship> builder)
    {
        builder
            .HasKey(ship => ship.Id);
    }
}