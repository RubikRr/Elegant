﻿using Elegant.Abstraction.Handlers.Command;
using Elegant.Abstraction.Handlers.Query;
using Elegant.Business;
using Elegant.Business.Handlers.Product.Command.AddProduct;
using Elegant.Business.Handlers.Product.Command.RemoveProductById;
using Elegant.Business.Handlers.Product.Command.UpdateProduct;
using Elegant.Business.Handlers.Product.Query.GetAllProducts;
using Elegant.Business.Handlers.Product.Query.GetProductById;
using Elegant.Business.Models.ViewModels.Product;
using Elegant.Core.Models;
using Elegant.DAL;
using Elegant.DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Elegant.Web.Areas.Admin.Controllers;

[Area(DbConstants.AdminRoleName)]
[Authorize(Roles = DbConstants.AdminRoleName)]
public class ProductController : Controller
{
    private readonly IWebHostEnvironment _appEnvironment;
    private readonly IQueryHandler<GetAllProductsRequest, GetAllProductsResponse> _getAllProductsRequestHandler;
    private readonly IQueryHandler<GetProductByIdRequest, GetProductByIdResponse> _getProductByIdQueryHandler;


    private readonly ICommandHandler<AddProductRequest, AddProductResponse> _addProductRequestHandler;
    private readonly ICommandHandler<UpdateProductRequest, UpdateProductResponse> _updateProductRequestHandler;
    private readonly ICommandHandler<RemoveProductByIdRequest, RemoveProductByIdResponse> _removeProductByIdRequestHandler;


    public ProductController(IWebHostEnvironment appEnvironment,
        IQueryHandler<GetAllProductsRequest, GetAllProductsResponse> getAllProductsRequestHandler,
        ICommandHandler<RemoveProductByIdRequest, RemoveProductByIdResponse> removeProductByIdRequestHandler,
        IQueryHandler<GetProductByIdRequest, GetProductByIdResponse> getProductByIdQueryHandler,
        ICommandHandler<AddProductRequest, AddProductResponse> addProductRequestHandler,
        ICommandHandler<UpdateProductRequest, UpdateProductResponse> updateProductRequestHandler)
    {
        _appEnvironment = appEnvironment;
        _getAllProductsRequestHandler = getAllProductsRequestHandler;
        _removeProductByIdRequestHandler = removeProductByIdRequestHandler;
        _getProductByIdQueryHandler = getProductByIdQueryHandler;
        _addProductRequestHandler = addProductRequestHandler;
        _updateProductRequestHandler = updateProductRequestHandler;
    }

    public async Task<IActionResult> GetAllProducts(CancellationToken cancellationToken = default)
    {
        var response = await _getAllProductsRequestHandler.HandleAsync(new GetAllProductsRequest(), cancellationToken);
        return View(nameof(GetAllProducts), response.Products);
    }

    public async Task<IActionResult> Remove(Guid productId, CancellationToken cancellationToken = default)
    {
        await _removeProductByIdRequestHandler.HandleAsync(new RemoveProductByIdRequest { ProductId = productId },
            cancellationToken);
        return RedirectToAction(nameof(GetAllProducts));
    }

    [HttpGet]
    public IActionResult AddProduct()
    {
        return View(nameof(AddProduct));
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct(CreateProductViewModel product, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(nameof(AddProduct));
        }

        var productImageDirectoryPath = Path.Combine(_appEnvironment.WebRootPath + Constants.ProductImageDirectoryPath);

        await _addProductRequestHandler.HandleAsync(
            new AddProductRequest { ViewModel = product, ProductImageDirectoryPath = productImageDirectoryPath }, cancellationToken);

        return RedirectToAction(nameof(GetAllProducts));
    }

    public async Task<IActionResult> UpdateProduct(Guid productId, CancellationToken cancellationToken = default)
    {
        var response =
            await _getProductByIdQueryHandler.HandleAsync(new GetProductByIdRequest { ProductId = productId, },
                cancellationToken);
        var editedProduct = new UpdateProductViewModel
        {
            Id = response.Product.Id,
            Name = response.Product.Name,
            Cost = response.Product.Cost,
            Description = response.Product.Description,
        };
        return View(nameof(UpdateProduct), editedProduct);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateProduct(UpdateProductViewModel product, CancellationToken cancellationToken)
    {
        var productImageDirectoryPath = Path.Combine(_appEnvironment.WebRootPath + Constants.ProductImageDirectoryPath);
        
        if (!ModelState.IsValid)
        {
            return View(nameof(UpdateProduct));
        }
        await _updateProductRequestHandler.HandleAsync(new UpdateProductRequest { ViewModel = product, ProductImageDirectoryPath = productImageDirectoryPath }, cancellationToken);
        
        return RedirectToAction(nameof(GetAllProducts));
    }
}