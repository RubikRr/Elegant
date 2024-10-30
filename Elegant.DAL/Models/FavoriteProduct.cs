namespace Elegant.DAL.Models
{
    public class FavoriteProduct
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public Product Product { get; set; }
    }
}
