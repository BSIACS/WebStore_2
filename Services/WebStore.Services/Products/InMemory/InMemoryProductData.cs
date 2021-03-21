using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Interfaces;
using WebStore.Services.Data;

namespace WebStore.Services.Products.InMemory
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Brand> GetBrands() => TestData.Brands;

        public IEnumerable<Section> GetSections() => TestData.Sections;

        public IEnumerable<Product> GetProducts(ProductFilter productFilter)
        {
            var query = TestData.Products;

            if (productFilter?.SectionId != null)
                query = query.Where(product => product.SectionId == productFilter.SectionId);

            if (productFilter?.BrandId != null)
                query = query.Where(product => product.BrandId == productFilter.BrandId);

            return query;
        }

        public Product GetProductById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
