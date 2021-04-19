using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Interfaces;
using WebStore.Services.Mapping;
using WebStore.Domain.DTO.Orders;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            List<Order> orders = _orderService.GetOrders().FromDTO().ToList();

            return View(orders.ToViewModels());
        }

        public async Task<IActionResult> OrderDetail(int id)
        {
            OrderDTO orderDto = await _orderService.GetOrderById(id);

            Order order = orderDto.FromDTO();

            OrderDetailViewModel viewModel = new OrderDetailViewModel
            {
                Id = order.Id,
                Address = order.Address,
                Phone = order.Phone,
                Cart = order.ToCartViewModel(),
                User = order.User.ToViewModel()
            };

            if (order is null)
                return NotFound();

            return View(viewModel);
        }
    }
}
