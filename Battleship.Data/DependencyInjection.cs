using Battleship.Core.Interfaces.Repositories;
using Battleship.Data.Cache;
using Battleship.Data.Cache.Interfaces;
using Battleship.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Battleship.Data;

public static class DependencyInjection
{
    public static void AddDataAccessLayer(this IServiceCollection services)
    {
        services.AddSingleton<IGameCache, GameCache>();
        
        services.AddTransient<IGameRepository, GameRepository>();
    }
}