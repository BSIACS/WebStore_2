using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain.DTO.Orders;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.WebStore
{
    public class OrdersClient : BaseClient, IOrderService
    {
        public OrdersClient(IConfiguration configuration) : base(configuration, "/api/OrdersApi")
        {
        }

        public async Task<OrderDTO> CreateOrder(CreateOrderModel model)
        {
            var response = await PostAsync<CreateOrderModel>(Address, model);
            return await response.Content.ReadAsAsync<OrderDTO>();
        }

        public void DeleteOrder(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderDTO> GetOrderById(int id)
        {
            var result = await GetAsync<OrderDTO>($"{Address}/{id}");

            return result;
        }

        public IEnumerable<OrderDTO> GetOrders()
        {
            return Get<IEnumerable<OrderDTO>>(Address);
        }

        public IEnumerable<OrderDTO> GetUserOrders(string UserName)
        {
            throw new NotImplementedException();
        }
    }
}
