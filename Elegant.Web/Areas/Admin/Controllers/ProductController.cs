using Elegant.Abstraction.Handlers.Command;
using Elegant.Abstraction.Handlers.Query;
using Elegant.Business.Handlers.Product.Command.AddProduct;
using Elegant.Business.Handlers.Product.Command.RemoveProductById;
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
    private readonly IProductsStorage _productsStorage;
    private readonly IWebHostEnvironment _appEnvironment;
    private readonly IQueryHandler<GetAllProductsRequest, GetAllProductsResponse> _getAllProductsRequestHandler;
    private readonly IQueryHandler<GetProductByIdRequest, GetProductByIdResponse> _getProductByIdQueryHandler;

    private readonly ICommandHandler<RemoveProductByIdRequest, RemoveProductByIdResponse> _removeProductByIdRequestHandler;

    private readonly ICommandHandler<AddProductRequest, AddProductResponse> _addProductRequestHandler;

    public ProductController(IProductsStorage productsStorage, IWebHostEnvironment appEnvironment,
        IQueryHandler<GetAllProductsRequest, GetAllProductsResponse> getAllProductsRequestHandler,
        ICommandHandler<RemoveProductByIdRequest, RemoveProductByIdResponse> removeProductByIdRequestHandler,
        IQueryHandler<GetProductByIdRequest, GetProductByIdResponse> getProductByIdQueryHandler,
        ICommandHandler<AddProductRequest, AddProductResponse> addProductRequestHandler)
    {
        _productsStorage = productsStorage;
        _appEnvironment = appEnvironment;
        _getAllProductsRequestHandler = getAllProductsRequestHandler;
        _removeProductByIdRequestHandler = removeProductByIdRequestHandler;
        _getProductByIdQueryHandler = getProductByIdQueryHandler;
        _addProductRequestHandler = addProductRequestHandler;
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

        await _addProductRequestHandler.HandleAsync(new AddProductRequest { ViewModel = product }, cancellationToken);

        return RedirectToAction(nameof(GetAllProducts));
    }

    public async Task<IActionResult> Update(Guid productId, CancellationToken cancellationToken = default)
    {
        var response =
            await _getProductByIdQueryHandler.HandleAsync(new GetProductByIdRequest { ProductId = productId },
                cancellationToken);
        var editedProduct = new EditProductViewModel
        {
            Id = response.Product.Id,
            Name = response.Product.Name,
            Cost = response.Product.Cost,
            Description = response.Product.Description,
        };
        return View(nameof(Update), editedProduct);
    }

    [HttpPost]
    public IActionResult Update(EditProductViewModel product, CancellationToken cancellationToken)
    {
        var productImagePath = Path.Combine(_appEnvironment.WebRootPath + "/images/products/");
        var fileName = Guid.NewGuid() + "." + product.UploadedImage.FileName.Split('.').Last();
        using (var fileStream = new FileStream(productImagePath + fileName, FileMode.Create))
        {
            product.UploadedImage.CopyTo(fileStream);
        }

        product.ImagePath = "/images/products/" + fileName;
        _productsStorage.Update(new Product
        {
            Id = product.Id,
            Name = product.Name,
            Cost = product.Cost,
            Description = product.Description,
        }, cancellationToken);

        return RedirectToAction("GetAllProducts");
    }
}