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

    public Task<List<Product>> GetAll() => _dbContext
        .Products
        .Include(product => product.CartItems)
        .Include(product => product.ImageItems)
        .AsSplitQuery()
        .ToListAsync();

    public async Task Add(Product product)
    {
        _dbContext.Products.Add(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(Product product)
    {
        var productInStorage = await GetById(product.Id);
        if (productInStorage != null)
        {
            productInStorage.Name = product.Name;
            productInStorage.Cost = product.Cost;
            productInStorage.Description = product.Description;
            productInStorage.ImagePath = product.ImagePath;
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Product>> Search(string name)
    {
        return string.IsNullOrWhiteSpace(name)
            ? new List<Product>()
            : await _dbContext.Products.Where(product => product.Name.ToLower().StartsWith(name.ToLower())).ToListAsync();
    }

    public async Task Remove(Guid productId)
    {
        var product = await GetById(productId);
        if (product == null)
        {
            return;
        }

        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Product?> GetById(Guid id)
    {
        return await _dbContext.Products
            .Include(product => product.CartItems)
            .Include(product => product.ImageItems)
            .AsSplitQuery()
            .FirstOrDefaultAsync(pr => pr.Id == id);
    }
}