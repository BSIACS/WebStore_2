using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entities;
using WebStore.Domain.Identity;
using WebStore.Domain.ViewModels;

namespace WebStore.Domain.DTO.Orders
{
    public record OrderItemDTO(int Id, ProductDTO Product, decimal Price, int Quantity);

    public record OrderDTO(int Id, User User, string Address, string Phone, DateTime Date, IEnumerable<OrderItemDTO> OrderItems);

    public record CreateOrderModel(OrderViewModel Order, IList<OrderItemDTO> Items);
}
