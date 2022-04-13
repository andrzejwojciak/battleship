using Battleship.Core.Interfaces.ProxyRepositories;
using Battleship.Core.Interfaces.Repositories;
using Battleship.Data.Cache;
using Battleship.Data.Cache.Interfaces;
using Battleship.Data.ProxyRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace Battleship.Data;

public static class DependencyInjection
{
    public static void AddDataAccessLayer(this IServiceCollection services)
    {      
        services.AddDbContext<BattleshipDbContext>(
            options =>
            {
                options.UseInMemoryDatabase("BattleshipInMemoryDb");
                options.ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });
        
        services.AddScoped<IBattleshipDbContext>(provider => provider.GetService<BattleshipDbContext>()!);

        services.AddTransient<IGameProxyRepository, GameProxyRepository>();
        
        services.AddSingleton<IGameCache, GameCache>();
    }
}