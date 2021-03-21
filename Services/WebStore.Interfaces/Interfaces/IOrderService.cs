using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Orders;
using WebStore.Domain.ViewModels;

namespace WebStore.Interfaces.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<Order> GetUserOrders(string UserName);

        Task<Order> GetOrderById(int id);

        Task CreateOrder(OrderViewModel viewModel);

        void DeleteOrder(int id);
    }
}
