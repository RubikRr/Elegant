using Elegant.Abstraction.Handlers.Command;
using Elegant.DAL.Interfaces;

namespace Elegant.Business.Handlers.Product.Command.AddProduct;

public class AddProductCommandHandler : ICommandHandler<AddProductRequest, AddProductResponse>
{
    private readonly IProductsStorage _productsStorage;

    public AddProductCommandHandler(IProductsStorage productsStorage)
    {
        _productsStorage = productsStorage;
    }

    public async Task<AddProductResponse> HandleAsync(AddProductRequest command, CancellationToken cancellationToken = default)
    {
         var newProduct = new Core.Models.Product
         {
             Id = Guid.NewGuid(),
             Name = command.ViewModel.Name,
             Cost = command.ViewModel.Cost,
             Description = command.ViewModel.Description,
         };
        
         await _productsStorage.Add(newProduct, cancellationToken);
        return new AddProductResponse();
    }
}

// var a = WebHostEnvironmentExtensions.GetPathForProductImages();
//  if (!Directory.Exists(productImagePath))
//  {
//      Directory.CreateDirectory(productImagePath);
//  }
//
//  var imageItems = new List<Image>();
//  foreach (var image in product.UploadedImage)
//  {
//      var fileName = Guid.NewGuid() + "." + image.FileName.Split('.').Last();
//      using (var fileStream = new FileStream(productImagePath + fileName, FileMode.Create))
//      {
//          image.CopyTo(fileStream);
//      }
//
//      imageItems.Add(new Image
//      {
//          ImagePath = "/images/products/" + fileName,
//          Product = null
//      });
//  }