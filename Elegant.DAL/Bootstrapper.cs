using Elegant.DAL.Interfaces;
using Elegant.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Elegant.DAL;

public static class Bootstrapper
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<IFavoritesStorage, DbFavoritesStorage>();
        services.AddTransient<ICartRepository, CartRepository>();
        services.AddTransient<IOrdersStorage, DbOrdersStorage>();
    }
}