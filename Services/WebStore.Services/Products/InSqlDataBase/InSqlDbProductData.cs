using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Interfaces;
using WebStore.Services.Mapping;

namespace WebStore.Services.Products.InSqlDataBase
{
    public class InSqlDbProductData : IProductData
    {
        readonly private WebStoreDB _dB;

        public InSqlDbProductData(WebStoreDB dB)
        {
            _dB = dB;
        }

        public IEnumerable<BrandDTO> GetBrands() => _dB.Brands.Include(brand => brand.Products).ToDTO();

        public ProductDTO GetProductById(int id)
        {
            return _dB.Products
                .Include(p => p.Brand)
                .Include(p => p.Section)
                .FirstOrDefault(p => p.Id == id).ToDTO();
        }

        public IEnumerable<ProductDTO> GetProducts(ProductFilter productFilter = null)
        {
            IQueryable<Product> products = _dB.Products.Include(p => p.Brand).Include(p => p.Section);

            if (productFilter?.ProductsIds?.Length > 0)
            {
                if (productFilter?.ProductsIds?.Length > 0)
                {
                    products = products.Where(p => productFilter.ProductsIds.Contains(p.Id));
                }
            }
            else
            {
                if (productFilter?.BrandId != null)
                    products = products.Where(p => p.BrandId == productFilter.BrandId);

                if (productFilter?.SectionId != null)
                    products = products.Where(p => p.SectionId == productFilter.SectionId);
            }

            return products.AsEnumerable().ToDTO();
        }

        public IEnumerable<SectionDTO> GetSections() => _dB.Sections.Include(section => section.Products).ToDTO();


    }
}
