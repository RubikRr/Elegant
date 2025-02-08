using Elegant.Abstraction.Handlers.Query;
using Elegant.Business.Handlers.Product.Query.GetProductById;
using Microsoft.Extensions.DependencyInjection;

namespace Elegant.Business;

public static class Bootstrapper
{
    public static void AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<IQueryHandler<GetProductByIdRequest,GetProductByIdResponse>, GetProductByIdQueryHandler>();
    }

    // public static void AddRepositories(this IServiceCollection services)
    // {
    //     services.AddTransient<IProductsStorage, DbProductsStorage>();
    //     services.AddTransient<IFavoritesStorage, DbFavoritesStorage>();
    //     services.AddTransient<ICartsStorage, DbCartsStorage>();
    //     services.AddTransient<IOrdersStorage, DbOrdersStorage>();
    // }
}