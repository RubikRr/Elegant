using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace WomanShop.Models
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string Description { get;set;}
        public List<string> ImageItemsPaths { get; set; }
        public string ImagePath { get; set; }
    }
}
