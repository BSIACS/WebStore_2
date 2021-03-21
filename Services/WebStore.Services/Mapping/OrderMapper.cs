using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Orders;
using WebStore.Domain.ViewModels;

namespace WebStore.Services.Mapping
{
    public static class OrderMapper
    {
        public static OrderViewModel ToViewModel(this Order order)
        {
            OrderViewModel viewModel = new OrderViewModel
            {
                Id = order.Id,
                Name = order.Name,
                Address = order.Address,
                Phone = order.Phone,
                Date = order.Date
            };

            return viewModel;
        }

        public static IEnumerable<OrderViewModel> ToViewModels(this IEnumerable<Order> orders)
        {
            IEnumerable<OrderViewModel> viewModels = orders.Select(o => o.ToViewModel());

            return viewModels;
        }

        public static CartViewModel ToCartViewModel(this Order order)
        {
            CartViewModel cartViewModel = new CartViewModel() { Items = new List<(ProductViewModel, int)>() };

            foreach (var item in order.Items)
            {
                (ProductViewModel, int) cartItem = (
                    new ProductViewModel()
                    {
                        Id = item.Product.Id,
                        Name = item.Product?.Name,
                        ImageUrl = item.Product.ImageUrl,
                        Price = item.Product.Price,
                        Brand = item.Product.Brand.Name
                    },
                    item.Quantity);
                (cartViewModel.Items as List<(ProductViewModel, int)>).Add(cartItem);
            }

            return cartViewModel;
        }
    }
}
