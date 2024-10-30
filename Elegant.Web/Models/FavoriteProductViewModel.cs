using OnlineShop.DB.Models;

namespace Elegant.Web.Models
{
    public class FavoriteProductViewModel
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public Product Product { get; set; }
    }
}
