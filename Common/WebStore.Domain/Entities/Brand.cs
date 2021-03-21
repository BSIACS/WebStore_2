using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities
{
    [Table("Brands")]
    public class Brand : NamedEntity, IOrderedEntity
    {
        public int Order { set; get; }

        public ICollection<Product> Products { get; set; }
    }

}
