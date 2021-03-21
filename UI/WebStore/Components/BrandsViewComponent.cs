using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Interfaces;

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
                Quantity = item.Products.Count,
            });

            return View(brandsViewModels);
        }


    }
}
