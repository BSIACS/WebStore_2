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

        public IViewComponentResult Invoke()
        {
            var brandsViewModels = _productData.GetBrands().Select(item => new BrandViewModel() { 
                Id = item.Id,
                Name = item.Name,
                Quantity = item.ProductsCount
            });

            return View(brandsViewModels);
        }


    }
}
