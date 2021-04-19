using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CatalogueController : Controller
    {
        readonly private IProductData _productData;

        public CatalogueController(IProductData productData)
        {
            _productData = productData;
        }

        public IActionResult Index()
        {
            var products = _productData.GetProducts();

            return View(products.FromDTO());
        }

        [HttpGet]
        public IActionResult Edit(int id) {
            var product = _productData.GetProductById(id);

            if (product == null)
                return NotFound();

            return View(product.FromDTO().ToViewModel());
        }

        [HttpPost]
        public IActionResult Edit(ProductViewModel model) {

            if (!ModelState.IsValid)
                return View(model);

            //Дописать логику

            return RedirectToAction("Index");
        }
    }
}
