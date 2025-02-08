using Elegant.DAL.Interfaces;
using Elegant.DAL.Storages;
using Microsoft.Extensions.DependencyInjection;

namespace Elegant.DAL;

public static class Bootstrapper
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IProductsStorage, DbProductsStorage>();
        services.AddTransient<IFavoritesStorage, DbFavoritesStorage>();
        services.AddTransient<ICartsStorage, DbCartsStorage>();
        services.AddTransient<IOrdersStorage, DbOrdersStorage>();
    }
}