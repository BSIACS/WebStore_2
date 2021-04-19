using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.DTO.Products;
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

        public static ProductDTO ToDTO(this Product product) {
            return product == null ? null : new ProductDTO(
                product.Id, product.Name, product.Order, product.Price, product.ImageUrl, product.Brand.ToDTO(), product.Section.ToDTO()
            );
        }

        public static Product FromDTO(this ProductDTO productDTO) {
            return productDTO == null ? null : new Product
            {
                Id = productDTO.Id,
                Name = productDTO.Name,
                Order = productDTO.Order,
                Price = productDTO.Price,
                ImageUrl = productDTO.ImageUrl,
                Brand = productDTO.Brand.FromDTO(),
                BrandId = productDTO.Brand.Id,
                Section = productDTO.Section.FromDTO(),
                SectionId = productDTO.Section.Id
            };
        }

        public static IEnumerable<ProductDTO> ToDTO(this IEnumerable<Product> products) {
            return products?.Select(ToDTO);
        }

        public static IEnumerable<Product> FromDTO(this IEnumerable<ProductDTO> productsDTO)
        {
            return productsDTO == null ? null : productsDTO.Select(FromDTO);
        }
    }
}
