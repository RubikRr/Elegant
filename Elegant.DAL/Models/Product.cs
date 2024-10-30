
namespace Elegant.DAL.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public List<ImageItem> ImageItems { get; set; }
        public List<CartItem> CartItems { get; set; }

        public Product()
        {
            CartItems = new List<CartItem>();
            ImageItems = new List<ImageItem>();
        }
    }
}
