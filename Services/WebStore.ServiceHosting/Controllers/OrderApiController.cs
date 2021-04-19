using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.DTO.Orders;
using WebStore.Domain.Entities.Orders;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Interfaces;

namespace WebStore.ServiceHosting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersApiController : ControllerBase, IOrderService
    {
        private readonly IOrderService _orderService;

        public OrdersApiController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IEnumerable<OrderDTO> GetOrders()
        {
            return _orderService.GetOrders();
        }

        [HttpGet("{id}")]
        public async Task<OrderDTO> GetOrderById(int id)
        {
            var result = await _orderService.GetOrderById(id);

            return result;
        }

        [HttpPost]
        public async Task<OrderDTO> CreateOrder(CreateOrderModel model)
        {
            return await _orderService.CreateOrder(model);
        }

        [HttpGet("delete/{id}")]
        public void DeleteOrder(int id)
        {
            _orderService.DeleteOrder(id);
        }

        [HttpGet("UserName")]
        public IEnumerable<OrderDTO> GetUserOrders(string UserName)
        {
            return _orderService.GetUserOrders(UserName);
        }
    }
}
