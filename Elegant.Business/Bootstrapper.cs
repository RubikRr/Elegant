using Elegant.Abstraction.Handlers.Command;
using Elegant.Abstraction.Handlers.Query;
using Elegant.Business.Handlers.Product.Command.AddProduct;
using Elegant.Business.Handlers.Product.Command.RemoveProductById;
using Elegant.Business.Handlers.Product.Command.UpdateProduct;
using Elegant.Business.Handlers.Product.Query.GetAllProducts;
using Elegant.Business.Handlers.Product.Query.GetProductById;
using Elegant.Business.Handlers.Product.Query.GetProductsByName;
using Microsoft.Extensions.DependencyInjection;

namespace Elegant.Business;

public static class Bootstrapper
{
    public static void AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<IQueryHandler<GetProductByIdRequest, GetProductByIdResponse>, GetProductByIdQueryHandler>();
        services.AddScoped<IQueryHandler<GetAllProductsRequest, GetAllProductsResponse>, GetAllProductsQueryHandler>();
        services.AddScoped<IQueryHandler<GetProductsByNameRequest, GetProductsByNameResponse>, GetProductsByNameQueryHandler>();

        services.AddScoped<ICommandHandler<AddProductRequest, AddProductResponse>, AddProductCommandHandler>();
        services.AddScoped<ICommandHandler<UpdateProductRequest, UpdateProductResponse>, UpdateProductCommandHandler>();
        services.AddScoped<ICommandHandler<RemoveProductByIdRequest, RemoveProductByIdResponse>, RemoveProductByIdCommandHandler>();
    }
}