using Microsoft.EntityFrameworkCore;
using OnlineShop.DB.Interfaces;
using OnlineShop.DB.Models;

namespace OnlineShop.DB.Storages
{
    public class DbProductsStorage : IProductsStorage
    {
        //private readonly List<Product> products = new List<Product>()
        //{
        //    new Product("Пиджак",2500,"Женский пиджак","/images/products/image1.png"),
        //    new Product("Кеды",5000,"Кеды на лето","/images/products/image2.png"),
        //    new Product("Блузка",3000,"Блузка для офиса","/images/products/image3.png"),
        //    new Product("Платье",5000,"Платье на вечер","/images/products/image4.png"),
        //    new Product("Сапоги",5000,"Сапоги на лето","/images/products/image5.jpg"),
        //    new Product("Майка",5000,"Майка с Пухлей", "/images/products/image6.jpg"),
        //    new Product("Платье",4000,"Женское платье","/images/products/image7.jpg"),
        //    new Product("Кеды",5000,"Кеды на лето","/images/products/image1.png")
        //};
        private DatabaseContext dbContext;
        public DbProductsStorage(DatabaseContext _databaseContext)
        {
            dbContext = _databaseContext;
        }

        public List<Product> GetAll() => dbContext.Products.Include(product=>product.CartItems).ToList();
        public void Add(Product product)
        {
            dbContext.Products.Add(product);
            dbContext.SaveChanges();
        }
        public void Update(Product product)
        {
            var productInStorage = TryGetById(product.Id);
            productInStorage.Name = product.Name;
            productInStorage.Cost = product.Cost;
            productInStorage.Description = product.Description;
            productInStorage.ImagePath = product.ImagePath;
            dbContext.SaveChanges();
        }
        public List<Product> Search(string name)
        {
            return dbContext.Products.Where(product => product.Name.ToLower().StartsWith(name.ToLower())).ToList();
        }
        public void Remove(Guid productId)
        {
            var product=TryGetById(productId);
            dbContext.Products.Remove(product);
            dbContext.SaveChanges() ;
        }
        public Product TryGetById(Guid id)
        {
            return dbContext.Products.Include(product=>product.CartItems).FirstOrDefault(pr => pr.Id == id);
        }
    }
}