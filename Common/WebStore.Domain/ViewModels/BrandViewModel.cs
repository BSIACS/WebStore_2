using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.ViewModels
{
    public class BrandViewModel : INamedEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }
    }
}
