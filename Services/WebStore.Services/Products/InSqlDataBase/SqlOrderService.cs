using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.DTO.Orders;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Orders;
using WebStore.Domain.Identity;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Services.Products.InSqlDataBase
{
    public class SqlOrderService : IOrderService
    {
        private readonly WebStoreDB _dB;
        private readonly UserManager<User> _userManager;

        public SqlOrderService(WebStoreDB dB, ICartService cart, UserManager<User> userManager)
        {
            _dB = dB;
            _userManager = userManager;
        }

        public IEnumerable<OrderDTO> GetOrders() {
            return _dB.Orders.Include(o => o.User).ToList().ToDTO();
        }

        public IEnumerable<OrderDTO> GetUserOrders(string userName)
        {
            List<OrderDTO> orders = _dB.Orders
                .Include(o => o.Items)
                .Include(o => o.User)
                .Where(o => o.User.UserName == userName)
                .ToDTO().ToList();

            return orders;
        }

        public async Task<OrderDTO> GetOrderById(int id)
        {
            Order order = await _dB.Orders
                .Include(o => o.User)
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .ThenInclude(p => p.Brand)

                .Include(x => x.Items).ThenInclude(x => x.Product).ThenInclude(s => s.Section) //!!!!!!!

                .FirstOrDefaultAsync(o => o.Id == id);

            return order.ToDTO();
        }

        
        public async Task<OrderDTO> CreateOrder(CreateOrderModel model)
        {
            User user = await _userManager.FindByIdAsync(model.Order.UserId);

            if (user == null)
                throw new InvalidOperationException($"User {model.Order.UserId} not found in database");

            Order order = new Order() { 
                User = user,
                Name = user.UserName,
                Address = model.Order.Address,
                Phone = model.Order.Phone,
                Date = model.Order.Date
            };

            await using var transaction = await _dB.Database.BeginTransactionAsync();

            List<OrderItem> items = new List<OrderItem>();

            foreach (var item in model.Items) {
                OrderItem orderItem = new OrderItem();
                orderItem.Order = order;
                orderItem.Product = await _dB.Products.FirstOrDefaultAsync(p => p.Id == item.Id);
                orderItem.Quantity = item.Quantity;
                orderItem.Price = item.Price;

                items.Add(orderItem);
            }

            order.Items = items;

            await _dB.Orders.AddAsync(order);

            await _dB.SaveChangesAsync();

            await transaction.CommitAsync();

            return order.ToDTO();
        }

        public void DeleteOrder(int id)
        {
            throw new NotImplementedException();
        }
    }
}
