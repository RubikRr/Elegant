using Elegant.Core.Models;
using Elegant.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Elegant.DAL.Storages;

public class DbOrdersStorage : IOrdersStorage
{
    private readonly EfCoreDbContext _dbContext;

    public DbOrdersStorage(EfCoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void Add(Order order)
    {
        _dbContext.Orders.Add(order);
        _dbContext.SaveChanges();
    }
    public List<Order> GetAll()
    {
        return _dbContext.Orders.Include(order => order.Items).ThenInclude(items => items.Product).Include(x => x.DeliveryInfo).ToList();
    }

    public Order TryGetById(Guid id)
    {
        return _dbContext.Orders.Include(order => order.Items).ThenInclude(items => items.Product).Include(x => x.DeliveryInfo).FirstOrDefault(order => order.Id == id);
    }
    public void UpdateStatus(Guid id, OrderStatus newStatus)
    {
        var order = TryGetById(id);
        if (order != null)
        {
            order.Status = newStatus;
        }
        _dbContext.SaveChanges();
    }
}