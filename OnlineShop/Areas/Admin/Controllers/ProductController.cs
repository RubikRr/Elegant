using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB.Interfaces;
using OnlineShop.DB.Models;
using System;
using WomanShop.Areas.Admin.Models;
using WomanShop.Helpers;

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

            var str = "fasdf";
            if (ModelState.IsValid)
            {
                var productImagePath = Path.Combine(appEnvironment.WebRootPath + "/images/products/");
                if (!Directory.Exists(productImagePath))
                {
                    Directory.CreateDirectory(productImagePath);
                }
                var imageItems = new List<ImageItem>();
                foreach (var image in product.UploadedImage)
                {
                    var fileName = Guid.NewGuid() + "." + image.FileName.Split('.').Last();
                    using (var fileStream = new FileStream(productImagePath + fileName, FileMode.Create))
                    {
                        image.CopyTo(fileStream);
                    }
                    imageItems.Add(new ImageItem { ImagePath= "/images/products/" + fileName });
                }
              
                var newProduct = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = product.Name,
                    Cost = product.Cost,
                    Description = product.Description,
                    ImageItems= imageItems,
                    ImagePath = "/images/products/image1"
                };
                
                productsStorage.Add(newProduct);
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Update(Guid productId)
        {
            var product = productsStorage.TryGetById(productId);

            return View(new EditProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Cost = product.Cost,
                Description = product.Description,
                ImagePath = product.ImagePath,
            });
        }

        [HttpPost]
        public IActionResult Update(EditProductViewModel product)
        {
            if (product.UploadedImage != null)
            {
                var productImagePath = Path.Combine(appEnvironment.WebRootPath + "/images/products/");
                var fileName = Guid.NewGuid() + "." + product.UploadedImage.FileName.Split('.').Last();
                using (var fileStream = new FileStream(productImagePath + fileName, FileMode.Create))
                {
                    product.UploadedImage.CopyTo(fileStream);
                }
                product.ImagePath = "/images/products/" + fileName;
            }
            productsStorage.Update(new Product { Id = product.Id, Name = product.Name, Cost = product.Cost, Description = product.Description, ImagePath = product.ImagePath });

            return RedirectToAction("Index");
        }
    }
}
