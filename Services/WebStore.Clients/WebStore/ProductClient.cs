using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain;
using WebStore.Domain.DTO.Products;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.WebStore
{
    public class ProductClient : BaseClient, IProductData
    {
        public ProductClient(IConfiguration configuration) : base(configuration, "/api/ProductsApi")
        {
        }

        public IEnumerable<BrandDTO> GetBrands()
        {
            return Get<IEnumerable<BrandDTO>>($"{Address}/brands");
        }

        public ProductDTO GetProductById(int id)
        {
            return Get<ProductDTO>($"{Address}/{id}");
        }

        public IEnumerable<ProductDTO> GetProducts(ProductFilter productFilter = null)
        {
            if (productFilter == null)
                productFilter = new ProductFilter();
            return Post<ProductFilter>($"{Address}/products", productFilter).Content.ReadAsAsync<IEnumerable<ProductDTO>>().Result;
        }

        public IEnumerable<SectionDTO> GetSections()
        {
            return Get<IEnumerable<SectionDTO>>($"{Address}/sections");
        }
    }
}
