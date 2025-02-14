using Elegant.Abstraction.Handlers.Command;
using Elegant.Abstraction.Handlers.Query;
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

    private readonly ICommandHandler<RemoveProductByIdRequest, RemoveProductByIdResponse>
        _removeProductByIdRequestHandler;

    public ProductController(IProductsStorage productsStorage, IWebHostEnvironment appEnvironment,
        IQueryHandler<GetAllProductsRequest, GetAllProductsResponse> getAllProductsRequestHandler,
        ICommandHandler<RemoveProductByIdRequest, RemoveProductByIdResponse> removeProductByIdRequestHandler,
        IQueryHandler<GetProductByIdRequest, GetProductByIdResponse> getProductByIdQueryHandler)
    {
        _productsStorage = productsStorage;
        _appEnvironment = appEnvironment;
        _getAllProductsRequestHandler = getAllProductsRequestHandler;
        _removeProductByIdRequestHandler = removeProductByIdRequestHandler;
        _getProductByIdQueryHandler = getProductByIdQueryHandler;
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
    public IActionResult AddProduct(CreateProductViewModel product, CancellationToken cancellationToken)
    {
        if (ModelState.IsValid)
        {
            var productImagePath = Path.Combine(_appEnvironment.WebRootPath + "/images/products/");
            if (!Directory.Exists(productImagePath))
            {
                Directory.CreateDirectory(productImagePath);
            }

            var imageItems = new List<Image>();
            foreach (var image in product.UploadedImage)
            {
                var fileName = Guid.NewGuid() + "." + image.FileName.Split('.').Last();
                using (var fileStream = new FileStream(productImagePath + fileName, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }

                imageItems.Add(new Image
                {
                    ImagePath = "/images/products/" + fileName,
                    Product = null
                });
            }

            var newProduct = new Product
            {
                Id = Guid.NewGuid(),
                Name = product.Name,
                Cost = product.Cost,
                Description = product.Description,
                ImageItems = imageItems,
                ImagePath = "/images/products/image1"
            };

            _productsStorage.Add(newProduct, cancellationToken);
            return RedirectToAction("GetAllProducts");
        }

        return View();
    }

    public async Task<IActionResult> Update(Guid productId, CancellationToken cancellationToken = default)
    {
        var response = await _getProductByIdQueryHandler.HandleAsync(new GetProductByIdRequest { ProductId = productId }, cancellationToken);
        var editedProduct = new EditProductViewModel
        {
            Id = response.Product.Id,
            Name = response.Product.Name,
            Cost = response.Product.Cost,
            Description = response.Product.Description,
            ImagePath = response.Product.ImagePath,
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
            ImagePath = product.ImagePath
        }, cancellationToken);

        return RedirectToAction("GetAllProducts");
    }
}