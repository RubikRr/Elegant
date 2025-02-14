using Elegant.Core.Models;
using Elegant.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Elegant.DAL.Storages;

public class DbProductsStorage : IProductsStorage
{
    private readonly EfCoreDbContext _dbContext;

    public DbProductsStorage(EfCoreDbContext efCoreDbContext)
    {
        _dbContext = efCoreDbContext;
    }

    public async Task<List<Product>> GetAll(CancellationToken cancellationToken) =>await _dbContext
        .Products
        .Include(product => product.CartItems)
        .Include(product => product.ImageItems)
        .AsSplitQuery()
        .ToListAsync(cancellationToken: cancellationToken);

    public async Task Add(Product product, CancellationToken cancellationToken)
    {
        _dbContext.Products.Add(product);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Update(Product product, CancellationToken cancellationToken)
    {
        var productInStorage = await GetById(product.Id, cancellationToken);
        if (productInStorage != null)
        {
            productInStorage.Name = product.Name;
            productInStorage.Cost = product.Cost;
            productInStorage.Description = product.Description;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Product>> Search(string name, CancellationToken cancellationToken)
    {
        return string.IsNullOrWhiteSpace(name)
            ? new List<Product>()
            : await _dbContext.Products.Where(product => product.Name.ToLower().StartsWith(name.ToLower()))
                .ToListAsync(cancellationToken);
    }

    public async Task Remove(Guid productId, CancellationToken cancellationToken)
    {
        var product = await GetById(productId, cancellationToken);
        if (product == null)
        {
            return;
        }

        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Product?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Products
            .Include(product => product.CartItems)
            .Include(product => product.ImageItems)
            .AsSplitQuery()
            .FirstOrDefaultAsync(pr => pr.Id == id, cancellationToken);
    }
}