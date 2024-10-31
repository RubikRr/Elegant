using Elegant.DAL;
using Elegant.DAL.Interfaces;
using Elegant.DAL.Models;
using Elegant.Web.Areas.Admin.Models;
using Elegant.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Elegant.Web.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class ProductController : Controller
    {
        private readonly IProductsStorage _productsStorage;
        private readonly IWebHostEnvironment _appEnvironment;

        public ProductController(IProductsStorage productsStorage, IWebHostEnvironment appEnvironment)
        {
            _productsStorage = productsStorage;
            _appEnvironment = appEnvironment;
        }

        public IActionResult Index()
        {
            var products = _productsStorage.GetAll();
            return View(Mapping.ToProductsViewModel(products));
        }

        public IActionResult Remove(Guid productId)
        {
            _productsStorage.Remove(productId);
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
                var productImagePath = Path.Combine(_appEnvironment.WebRootPath + "/images/products/");
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

                    imageItems.Add(new ImageItem { ImagePath = "/images/products/" + fileName });
                }

                var newProduct = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = product.Name,
                    Cost = product.Cost,
                    Description = product.Description,
                    ImageItems = imageItems,
                    ImagePath = "/images/products/image1"
                };

                _productsStorage.Add(newProduct);
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Update(Guid productId)
        {
            var product = _productsStorage.TryGetById(productId);

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
            var productImagePath = Path.Combine(_appEnvironment.WebRootPath + "/images/products/");
            var fileName = Guid.NewGuid() + "." + product.UploadedImage.FileName.Split('.').Last();
            using (var fileStream = new FileStream(productImagePath + fileName, FileMode.Create))
            {
                product.UploadedImage.CopyTo(fileStream);
            }

            product.ImagePath = "/images/products/" + fileName;
            _productsStorage.Update(new Product
            {
                Id = product.Id, Name = product.Name, Cost = product.Cost, Description = product.Description,
                ImagePath = product.ImagePath
            });

            return RedirectToAction("Index");
        }
    }
}