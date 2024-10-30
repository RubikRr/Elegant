using Microsoft.EntityFrameworkCore;
using OnlineShop.DB.Models;
using System.Security.Cryptography.X509Certificates;
using Elegant.DAL.Interfaces;
using WomanShop.Models;

namespace OnlineShop.DB.Storages
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
