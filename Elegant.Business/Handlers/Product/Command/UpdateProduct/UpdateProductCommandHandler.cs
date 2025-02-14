using Elegant.Abstraction.Handlers.Command;
using Elegant.Core.Models;
using Elegant.DAL.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Elegant.Business.Handlers.Product.Command.UpdateProduct;

public class UpdateProductCommandHandler : ICommandHandler<UpdateProductRequest, UpdateProductResponse>
{
    private readonly IProductsStorage _productsStorage;

    public UpdateProductCommandHandler(IProductsStorage productsStorage)
    {
        _productsStorage = productsStorage;
    }

    public async Task<UpdateProductResponse> HandleAsync(UpdateProductRequest command, CancellationToken cancellationToken = default)
    {
        var updatedProduct = new Core.Models.Product
        {
            Id = command.ViewModel.Id,
            Name = command.ViewModel.Name,
            Cost = command.ViewModel.Cost,
            Description = command.ViewModel.Description
        };

        if (command.ViewModel.UploadedImage is not null)
        {
            await ResetPreviousImagesForProduct(updatedProduct.Id, command.ProductImageDirectoryPath, cancellationToken);
            UpdateImagesForProduct(updatedProduct, command.ViewModel.UploadedImage, command.ProductImageDirectoryPath);
        }

        await _productsStorage.UpdateAsync(updatedProduct, cancellationToken);

        return new UpdateProductResponse();
    }

    private void UpdateImagesForProduct(Core.Models.Product newProduct, IFormFile[] images, string productImageDirectoryPath)
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

    private async Task ResetPreviousImagesForProduct(Guid productId, string productImageDirectoryPath, CancellationToken cancellationToken = default)
    {
        var product = await _productsStorage.GetByIdAsync(productId, cancellationToken);

        if (product == null)
        {
            return;
        }

        foreach (var image in product.ImageItems)
        {
            var fileName = string.Join('.', image.ImagePath.Split('.', '/').TakeLast(2));
            var filename = productImageDirectoryPath + fileName;
            File.Delete(filename);
        }

        product.ImageItems.Clear();
    }
}