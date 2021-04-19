using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.DTO.Orders;
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
                //Name = order.User.UserName,
                UserId = order.User.Id,
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

        public static OrderItemDTO ToDTO(this OrderItem orderItem) {
            return orderItem == null ? null : new OrderItemDTO(orderItem.Id, orderItem.Product.ToDTO(), orderItem.Price, orderItem.Quantity);
        }

        public static OrderItem FromDTO(this OrderItemDTO orderItemDTO) {
            return orderItemDTO == null ? null : new OrderItem() { 
                Id = orderItemDTO.Id, 
                Product = orderItemDTO.Product.FromDTO(),
                Price = orderItemDTO.Price, 
                Quantity = orderItemDTO.Quantity
            };
        }

        public static OrderDTO ToDTO(this Order order) {
            return new OrderDTO(order.Id, order.User, order.Address, order.Phone, order.Date, order.Items.Select(ToDTO));
        }

        public static Order FromDTO(this OrderDTO orderDTO) {
            return new Order() {
                Id = orderDTO.Id,
                Address = orderDTO.Address,
                User = orderDTO.User,
                Phone = orderDTO.Phone,
                Date = orderDTO.Date,
                Items = orderDTO.OrderItems.Select(FromDTO)
            };
        }

        public static IEnumerable<Order> FromDTO(this IEnumerable<OrderDTO> orderDTOs) {
            return orderDTOs.Select(FromDTO);
        }

        public static IEnumerable<OrderDTO> ToDTO(this IEnumerable<Order> orders)
        {
            return orders.Select(ToDTO);
        }
    }
}
