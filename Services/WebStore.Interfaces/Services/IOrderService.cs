using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.DTO.Orders;
using WebStore.Domain.Entities.Orders;
using WebStore.Domain.ViewModels;

namespace WebStore.Interfaces.Services
{
    public interface IOrderService
    {
        IEnumerable<OrderDTO> GetOrders();

        IEnumerable<OrderDTO> GetUserOrders(string userName);

        Task<OrderDTO> GetOrderById(int id);

        Task<OrderDTO> CreateOrder(CreateOrderModel model);

        void DeleteOrder(int id);
    }
}
