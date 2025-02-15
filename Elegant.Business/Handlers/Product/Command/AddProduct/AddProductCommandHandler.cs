using Elegant.Abstraction.Handlers.Command;
using Elegant.Core.Models;
using Elegant.DAL.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Elegant.Business.Handlers.Product.Command.AddProduct;

public class AddProductCommandHandler : ICommandHandler<AddProductRequest, AddProductResponse>
{
    private readonly IProductRepository _productRepository;

    public AddProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
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

        if (command.ViewModel.UploadedImage is not null)
        {
            AddImagesForProduct(newProduct, command.ViewModel.UploadedImage, command.ProductImageDirectoryPath);
        }

        await _productRepository.AddAsync(newProduct, cancellationToken);
        return new AddProductResponse();
    }

    private void AddImagesForProduct(Core.Models.Product newProduct, IFormFile[] images, string productImageDirectoryPath)
    {
        foreach (var image in images)
        {
            var fileName = Guid.NewGuid() + "." + image.FileName.Split('.').Last();
            using (var fileStream = new FileStream(productImageDirectoryPath + fileName, FileMode.Create))
            {
                image.CopyTo(fileStream);
            }

            newProduct.ImageItems.Add(new Image { ImagePath = $"{Constants.ProductImageDirectoryPath}{fileName}" });
        }
    }
}