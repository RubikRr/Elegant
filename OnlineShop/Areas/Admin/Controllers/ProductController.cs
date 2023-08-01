using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OnlineShop.DB.Interfaces;
using OnlineShop.DB.Models;
using System.Data;
using WomanShop.Areas.Admin.Models;
using WomanShop.Helpers;
using WomanShop.Models;

namespace WomanShop.Areas.Admin.Controllers
{
    [Area(OnlineShop.DB.Constants.AdminRoleName)]
    [Authorize(Roles = OnlineShop.DB.Constants.AdminRoleName)]
    public class ProductController : Controller
    {
        private IProductsStorage productsStorage;
        private readonly IWebHostEnvironment appEnvironment;
        public ProductController(IProductsStorage productsStorage, IWebHostEnvironment appEnvironment)
        {
            this.productsStorage = productsStorage;
            this.appEnvironment = appEnvironment;
        }

        public IActionResult Index()
        {
            var products = productsStorage.GetAll();
            return View(Mapping.ToProductsViewModel(products));
        }
        public IActionResult Remove(Guid productId)
        {
            productsStorage.Remove(productId);
            return RedirectToAction("Index");
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(CreateProductViewModel product)
        {
            
            if (ModelState.IsValid)
            {
                var productImagePath = Path.Combine(appEnvironment.WebRootPath+"/images/products/");
                if (!Directory.Exists(productImagePath))
                {
                    Directory.CreateDirectory(productImagePath);
                }
                var fileName = Guid.NewGuid() +"."+ product.UploadedImage.FileName.Split('.').Last();
                using (var fileStream=new FileStream(productImagePath+fileName,FileMode.Create))
                {
                    product.UploadedImage.CopyTo(fileStream);
                }
                var newProduct = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = product.Name,
                    Cost = product.Cost,
                    Description = product.Description,
                    ImagePath = "/images/products/"+fileName,
                };
                productsStorage.Add(newProduct);
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Update(Guid productId)
        {
            var product = productsStorage.TryGetById(productId);
            return View(Mapping.ToProductViewModel(product));
        }

        [HttpPost]
        public IActionResult Update(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                productsStorage.Update(Mapping.ToProductModel(product));
                return RedirectToAction("Index");
            }
            return RedirectToAction("Update");
        }
    }
}
