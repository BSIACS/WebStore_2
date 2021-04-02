using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entities;

namespace WebStore.Services.Mapping
{
    public static class SectionMapper
    {
        public static SectionDTO ToDTO(this Section section) {
            return section == null ? null : new SectionDTO(
                section.Id,
                section.Name,
                section.Order,
                section.ParentId
            );
        }

        public static Section FromDTO(this SectionDTO sectionDTO) {
            return sectionDTO == null ? null : new Section { 
                Id = sectionDTO.Id,
                Name = sectionDTO.Name,
                Order = sectionDTO.Order,
                ParentId = sectionDTO.ParentId
            };
        }

        public static IEnumerable<SectionDTO> ToDTO(this IEnumerable<Section> sections)
        {
            return sections == null ? null : sections.Select(ToDTO);
        }

        public static IEnumerable<Section> FromDTO(this IEnumerable<SectionDTO> sectionsDTO)
        {
            return sectionsDTO == null ? null : sectionsDTO.Select(FromDTO);
        }

    }

    public static class BrandMapper
    {
        public static BrandDTO ToDTO(this Brand brand)
        {
            return brand == null ? null : new BrandDTO(
                brand.Id,
                brand.Name,
                brand.Order,
                brand.Products.Count
            );
        }

        public static Brand FromDTO(this BrandDTO brandDTO)
        {
            return brandDTO == null ? null : new Brand
            {
                Id = brandDTO.Id,
                Name = brandDTO.Name,
                Order = brandDTO.Order
            };
        }
        public static IEnumerable<BrandDTO> ToDTO(this IEnumerable<Brand> brands)
        {
            return brands == null ? null : brands.Select(ToDTO);
        }

        public static IEnumerable<Brand> FromDTO(this IEnumerable<BrandDTO> brandsDTO)
        {
            return brandsDTO == null ? null : brandsDTO.Select(FromDTO);
        }

    }

}
