using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Domain.ViewModels
{
    public class CartViewModel
    {
        public IEnumerable<(ProductViewModel product, int quantity)> Items { get; set; }

        public int ItemsCount() => Items?.Sum(i => i.quantity) ?? 0;

        public decimal TotalPrice() => Items?.Sum(i => i.quantity * i.product.Price) ?? 0M;
    }
}
