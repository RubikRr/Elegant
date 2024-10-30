namespace Elegant.DAL.Models
{
    public class ImageItem
    {
        public Guid Id { get; set; }
        public string ImagePath { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
