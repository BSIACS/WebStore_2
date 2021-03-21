using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities
{
    public class Product : NamedEntity, IOrderedEntity
    {
        //public string Name { get; set; }

        public int Order { get; set; }

        public int? SectionId { get; set; }

        [ForeignKey(nameof(SectionId))]
        public Section Section { get; set; }

        public int? BrandId { get; set; }

        [ForeignKey(nameof(BrandId))]
        public Brand Brand { get; set; }
        
        public string ImageUrl { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }
}
