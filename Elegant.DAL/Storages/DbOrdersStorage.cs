using Elegant.DAL.Interfaces;
using Elegant.DAL.Models;
using Microsoft.EntityFrameworkCore;
using OnlineShop.DB;

namespace Elegant.DAL.Storages
{
    public class DbOrdersStorage : IOrdersStorage
    {
        private DatabaseContext dbContext;

        public DbOrdersStorage(DatabaseContext _dbContext) { dbContext = _dbContext; }
        public void Add(Order order)
        {
            dbContext.Orders.Add(order);
            dbContext.SaveChanges();
        }
        public List<Order> GetAll() 
        { 
            return dbContext.Orders.Include(order => order.Items).ThenInclude(items=>items.Product).Include(x=>x.DeliveryInfo).ToList();
        }

        public Order TryGetById(Guid id)
        {
            return dbContext.Orders.Include(order => order.Items).ThenInclude(items => items.Product).Include(x => x.DeliveryInfo).FirstOrDefault(order => order.Id == id);
        }
        public void UpdateStatus(Guid id, OrderStatus newStatus)
        {
            var order = TryGetById(id);
            if (order != null)
            {
                order.Status = newStatus;
            }
            dbContext.SaveChanges();
        }
    }
}
