using Elegant.Business.Models.ViewModels.Product;
using Elegant.Core.Models;

namespace Elegant.Business.Mapping;

public static partial class Mapping
{
    public static ProductViewModel ToProductViewModel(Product product)
    {
        return new ProductViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Cost = product.Cost,
            Description = product.Description,
            ImageItemsPaths = product.ImageItems.Select(imageItem => imageItem.ImagePath).ToList(),
            ImagePath = product.ImagePath
        };
    }

    public static Product ToProductModel(ProductViewModel productViewModel)
    {
        return new Product
        {
            Id = productViewModel.Id,
            Name = productViewModel.Name,
            Cost = productViewModel.Cost,
            Description = productViewModel.Description,
            ImagePath = productViewModel.ImagePath
        };
    }

    public static IReadOnlyCollection<ProductViewModel> ToProductsViewModel(List<Product> productsModel)
    {
        return productsModel.Select(ToProductViewModel).ToList();
    }
}