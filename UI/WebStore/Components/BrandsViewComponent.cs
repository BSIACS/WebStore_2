using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Components
{
    public class BrandsViewComponent : ViewComponent
    {
        private readonly IProductData _productData;

        public BrandsViewComponent(IProductData productData)
        {
            _productData = productData;
        }

        public IViewComponentResult Invoke(string brandId)
        {
            var brandsViewModels = _productData.GetBrands().Select(item => new BrandViewModel() { 
                Id = item.Id,
                Name = item.Name,
                Quantity = item.ProductsCount
            });

            int? intBrandId = int.TryParse(brandId, out int result) ? result : (int?)null;
            ViewData["brandId"] = intBrandId;

            return View(brandsViewModels);
        }


    }
}
