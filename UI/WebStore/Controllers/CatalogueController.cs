using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Interfaces;
using WebStore.Services.Mapping;

namespace WebStore.Controllers
{
    public class CatalogueController : Controller
    {
        private readonly IProductData _productData;

        public CatalogueController(IProductData productData)
        {
            _productData = productData;
        }

        public IActionResult Shop(int? brandId, int? sectionId)
        {
            var filter = new ProductFilter()
            {
                BrandId = brandId,
                SectionId = sectionId,
            };

            var products = _productData.GetProducts(filter);

            return View(new CatalogueViewModel() { 
                SectionId = sectionId,
                BrandId = brandId,
                Products = products.OrderBy(item => item.Order).Select(item => new ProductViewModel { 
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price,
                    ImageUrl = item.ImageUrl,
                })
            });
        }

        public IActionResult Details(int id) {
            Product product = _productData.GetProductById(id).FromDTO();

            if (product is null)
                return NotFound();

            return View(product.ToViewModel());
        }
    }
}
