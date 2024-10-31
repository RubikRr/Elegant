namespace Elegant.DAL.Models
{
    public class FavoriteProduct
    {
        public Guid Id { get; init; }
        public int UserId { get; init; }
        public Product Product { get; init; }
    }
}
