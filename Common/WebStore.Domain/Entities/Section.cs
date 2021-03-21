using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities
{
    public class Section : NamedEntity, IOrderedEntity
    {
        public int Order { set;  get; }

        public int? ParentId { set; get; }

        [ForeignKey(nameof(ParentId))]
        public Section Parent { set; get; }

        public ICollection<Product> Products { set; get; }
    }

}
