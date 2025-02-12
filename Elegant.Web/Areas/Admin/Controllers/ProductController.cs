using Elegant.Abstraction.Handlers.Command;
using Elegant.Abstraction.Handlers.Query;
using Elegant.Business.Handlers.Product.Command.RemoveProductById;
using Elegant.Business.Handlers.Product.Query.GetAllProducts;
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

    private readonly ICommandHandler<RemoveProductByIdRequest, RemoveProductByIdResponse>
        _removeProductByIdRequestHandler;

    public ProductController(IProductsStorage productsStorage, IWebHostEnvironment appEnvironment,
        IQueryHandler<GetAllProductsRequest, GetAllProductsResponse> getAllProductsRequestHandler,
        ICommandHandler<RemoveProductByIdRequest, RemoveProductByIdResponse> removeProductByIdRequestHandler)
    {
        _productsStorage = productsStorage;
        _appEnvironment = appEnvironment;
        _getAllProductsRequestHandler = getAllProductsRequestHandler;
        _removeProductByIdRequestHandler = removeProductByIdRequestHandler;
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

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(CreateProductViewModel product, CancellationToken cancellationToken)
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
        var product = await _productsStorage.GetById(productId, cancellationToken);

        return View(new EditProductViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Cost = product.Cost,
            Description = product.Description,
            ImagePath = product.ImagePath,
        });
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