using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Domain.ViewModels
{
    public class CreateOrderViewModel
    {
        public OrderViewModel order { get; set; }

        public CartViewModel cart { get; set; }

        public CreateOrderViewModel(OrderViewModel order, CartViewModel cart)
        {
            this.order = order;
            this.cart = cart;
        }
    }
}
