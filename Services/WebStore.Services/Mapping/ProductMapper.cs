using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewModels;

namespace WebStore.Services.Mapping
{
    public static class ProductMapper
    {
        public static ProductViewModel ToViewModel(this Product product)
        {
            return new ProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Brand = product.Brand?.Name,
            };
        }

        public static IEnumerable<ProductViewModel> ToViewModels(this IEnumerable<Product> products) => products.Select(ToViewModel);
    }
}
