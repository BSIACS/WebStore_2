using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Domain.DTO.Products
{
    
    public record BrandDTO(int Id, string Name, int Order, int ProductsCount);

    public record SectionDTO(int Id, string Name, int Order, int? ParentId);

    public record ProductDTO(
        int Id, 
        string Name, 
        int Order, 
        decimal Price, 
        string ImageUrl, 
        BrandDTO Brand, 
        SectionDTO Section);
}
