using Battleship.Core.Interfaces.Services;
using Battleship.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Battleship.Core;

public static class DependencyInjection
{
    public static void AddCoreLayer(this IServiceCollection services)
    {
        services.AddScoped<IGameService, GameService>();
        services.AddScoped<IBoardService, BoardService>();
        services.AddScoped<IPlayerService, PlayerService>();
        services.AddScoped<IShipService, ShipService>();
        services.AddScoped<IBoardShipService, BoardShipService>();
        services.AddScoped<IRandomMoveService, RandomMoveService>();
    }
}