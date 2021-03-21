using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Orders;
using WebStore.Domain.Identity;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Interfaces;

namespace WebStore.Services.Products.InSqlDataBase
{
    public class SqlOrderService : IOrderService
    {
        private readonly WebStoreDB _dB;
        private readonly ICartService _cart;
        private readonly UserManager<User> _userManager;

        public SqlOrderService(WebStoreDB dB, ICartService cart, UserManager<User> userManager)
        {
            _dB = dB;
            _cart = cart;
            _userManager = userManager;
        }

        public IEnumerable<Order> GetUserOrders(string userName)
        {
            List<Order> orders = _dB.Orders
                .Include(o => o.Items)
                .Include(o => o.User)
                .Where(o => o.User.UserName == userName)
                .ToList();

            return orders;
        }

        public async Task<Order> GetOrderById(int id)
        {
            Order order = await _dB.Orders
                .Include(o => o.User)
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .ThenInclude(p => p.Brand)
                .FirstOrDefaultAsync(o => o.Id == id);

            return order;
        }

        public async Task CreateOrder(OrderViewModel viewModel)
        {
            Order order = await OrderViewModelToOrder(viewModel);

            if (!Validator.TryValidateObject(order, new ValidationContext(order), null))
                throw new InvalidOperationException("Модель Order не валидна");

            await _dB.Orders.AddAsync(order);

            await _dB.SaveChangesAsync();

            _cart.Clear();
        }

        public void DeleteOrder(int id)
        {
            throw new NotImplementedException();
        }

        private async Task<Order> OrderViewModelToOrder(OrderViewModel orderViewModel)
        {
            User user = await _userManager.FindByNameAsync(orderViewModel.Name);

            Order order = new Order();
            List<OrderItem> orderItems = await GetOrderItems(order);

            order.User = user;
            order.Name = orderViewModel.Name;
            order.Address = orderViewModel.Address;
            order.Phone = orderViewModel.Phone;
            order.Date = DateTime.Now;
            order.Items = orderItems;

            return order;
        }

        private async Task<List<OrderItem>> GetOrderItems(Order order)
        {
            CartViewModel cartViewModel = _cart.TransformToViewModel();

            List<OrderItem> orderItems = new List<OrderItem>();

            foreach (var cartItem in cartViewModel.Items)
            {
                Product product = await _dB.Products.FirstOrDefaultAsync(p => p.Id == cartItem.product.Id);

                if (product == null)
                    throw new InvalidOperationException("В процессе заполнения коллекции List<OrderItem> возникла ошибка Не найден товар в базе данных");

                OrderItem orderItem = new OrderItem
                {
                    Order = order,
                    Price = cartItem.product.Price,
                    Product = product,
                    Quantity = cartItem.quantity
                };

                orderItems.Add(orderItem);
            }

            return orderItems;
        }


    }
}
